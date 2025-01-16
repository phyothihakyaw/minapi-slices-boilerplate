using Boilerplate.PublicApi.Data;
using Boilerplate.PublicApi.Extensions;
using Boilerplate.PublicApi.Extensions.Mappers;

namespace Boilerplate.PublicApi.Features.Todo;

public static class GetAllTodos
{
    public record Response(
        Guid Id,
        string Title,
        string? Description,
        string Status,
        DateTime? DueDate,
        DateTime CreatedAt);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/todo", Handle)
                .WithName("Get all todos")
                .Produces<IEnumerable<Response>>()
                .WithTags("todo");
        }

        private IResult Handle(AppDbContext context)
        {
            var result = context.Todo.AsEnumerable();
            var records = result.MapToListDto();

            return Results.Ok(records);
        }
    }
}