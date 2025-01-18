using Boilerplate.PublicApi.Data;
using Boilerplate.PublicApi.Extensions;
using Boilerplate.PublicApi.Extensions.Mappers;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Boilerplate.PublicApi.Features.Todo;

public static class CreateTodo
{
    public record Request(string Title, string? Description, TodoStatusEnum Status, DateTime? DueDate);

    public record Response(Guid Id);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/todo", Handle)
                .WithName("Create new todo")
                .Produces<Guid>()
                .ProducesProblem(400)
                .WithTags("todo");
        }

        public static Results<Ok<Guid>, BadRequest> Handle(Request request, AppDbContext context)
        {
            var validator = new Validator();
            var result = validator.Validate(request);

            if (!result.IsValid) return TypedResults.BadRequest();

            var todo = request.MapToDomain();
            context.Todo.Add(todo);
            context.SaveChanges();

            return TypedResults.Ok(todo.Id);
        }
    }

    private class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(dto => dto.Title).NotEmpty().NotNull();
            RuleFor(dto => dto.Status).NotNull();
        }
    }
}