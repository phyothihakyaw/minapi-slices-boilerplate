using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace minapi_slices_boilerplate.FunctionaTests;

public class TodoTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public TodoTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Get_Endpoint_Returns_Success()
    {
        // Arrange
        var client = _factory.CreateClient(); // Creates an HTTP client

        // Act
        var response = await client.GetAsync("/api/myendpoint");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}