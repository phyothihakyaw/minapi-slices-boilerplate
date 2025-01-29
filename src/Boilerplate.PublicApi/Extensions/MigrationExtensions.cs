using Boilerplate.PublicApi.Data;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.PublicApi.Extensions;

public static class MigrationExtensions
{
    public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        return app;
    }
}