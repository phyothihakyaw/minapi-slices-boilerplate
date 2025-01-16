using Boilerplate.PublicApi.Constants;

namespace Boilerplate.PublicApi.Features.Todo;

public class Todo
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TodoStatusEnum Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; private set; } = Clock.CurrentServerTime;
}

public enum TodoStatusEnum : byte
{
    New = 1,
    InProgress = 2,
    Done = 3
}