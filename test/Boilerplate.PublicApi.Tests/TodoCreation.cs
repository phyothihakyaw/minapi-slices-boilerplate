using Boilerplate.PublicApi.Data;
using Boilerplate.PublicApi.Extensions.Mappers;
using Boilerplate.PublicApi.Features.Todo;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSubstitute;
using Shouldly;
using Xunit;

public class CreateTodoTests
{
    [Fact]
    public void Handle_ValidRequest_ShouldReturnOkWithGuid()
    {
        // Arrange
        var context = Substitute.For<AppDbContext>();
        var request = new CreateTodo.Request("Test Title", "Test Description", TodoStatusEnum.New, DateTime.Now);
        var todo = request.MapToDomain();
        context.Todo.Add(todo);
        context.When(x => x.SaveChanges()).DoNotCallBase();

        // Act
        var result = CreateTodo.Endpoint.Handle(request, context);

        // Assert
        result.ShouldBeOfType<Results<Ok<Guid>, BadRequest>>();
        result.Result.ShouldBeOfType<Ok<Guid>>();
        ((Ok<Guid>)result.Result).Value.ShouldBe(todo.Id);
    }

    [Fact]
    public void Handle_InvalidRequest_ShouldReturnBadRequest()
    {
        // Arrange
        var context = Substitute.For<AppDbContext>();
        var request = new CreateTodo.Request("", null, TodoStatusEnum.New, DateTime.Now); // Invalid request

        // Act
        var result = CreateTodo.Endpoint.Handle(request, context);

        // Assert
        result.ShouldBeOfType<Results<Ok<Guid>, BadRequest>>();
        result.Result.ShouldBeOfType<BadRequest>();
    }
}