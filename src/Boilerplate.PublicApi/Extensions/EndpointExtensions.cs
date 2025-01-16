﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Boilerplate.PublicApi.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors =
        [
            ..assembly.DefinedTypes.Where(type => type is
                {
                    IsAbstract: false,
                    IsInterface: false
                } && type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
        ];

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder? MapEndpoints(this WebApplication? app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app!.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder? builder = routeGroupBuilder is null ? app : routeGroupBuilder;
        foreach (var endpoint in endpoints) endpoint.MapEndpoint(builder);

        return app;
    }
}