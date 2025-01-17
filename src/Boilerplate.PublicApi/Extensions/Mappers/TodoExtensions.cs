using Boilerplate.PublicApi.Features.Todo;

namespace Boilerplate.PublicApi.Extensions.Mappers;

public static class TodoExtensions
{
    public static IEnumerable<GetAllTodos.Response> MapToListDto(this IEnumerable<Todo> todos)
    {
        return todos.Select(todo => new GetAllTodos.Response(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.Status.ToString(),
            todo.DueDate,
            todo.CreatedAt
        ));
    }

    public static Todo MapToDomain(this CreateTodo.Request request)
    {
        return new Todo
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request?.Description,
            Status = request!.Status,
            DueDate = request?.DueDate
        };
    }
}