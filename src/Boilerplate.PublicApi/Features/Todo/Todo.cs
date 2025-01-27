using Boilerplate.PublicApi.Constants;

namespace Boilerplate.PublicApi.Features.Todo;

public class Todo
{
    public Todo()
    {
        
    }

    public Todo(string title, string description, TodoStatusEnum status, DateTime? dueDate)
        :this()
    {
        Title = title;
        Description = description;
        Status = status;
        DueDate = dueDate;
    }
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TodoStatusEnum Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; private set; } = Clock.CurrentServerTime;
    
    // Factory
    public Todo New(string title, string description, TodoStatusEnum status, DateTime? dueDate)
    {
        // validation 
        return new Todo(title, description, status, dueDate) { Title = title };
    }
    
    // Behaviours
    public void UpdateTitle(string title)
    {
        // validation => business validation
        // 
        Title = title;
    }

    public void ChangeStatusToDone()
    {
        Status = TodoStatusEnum.Done;
    }
}

public enum TodoStatusEnum : byte
{
    New = 1,
    InProgress = 2,
    Done = 3
}