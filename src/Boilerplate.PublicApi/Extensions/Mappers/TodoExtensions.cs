using Boilerplate.PublicApi.Features.Todo;

namespace Boilerplate.PublicApi.Extensions.Mappers;

public static class TodoExtensions
{
    public static IEnumerable<GetAllTodos.Response> MapToListDto(this IEnumerable<Todo> todos)
    {
        foreach (var todo in todos)
            yield return new GetAllTodos.Response(
                todo.Id,
                todo.Title,
                todo.Description,
                todo.Status.ToString(),
                todo.DueDate,
                todo.CreatedAt
            );
    }
}