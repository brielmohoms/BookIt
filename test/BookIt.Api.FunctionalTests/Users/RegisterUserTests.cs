using System.Net;
using System.Net.Http.Json;
using BookIt.Api.Controllers.Users;
using BookIt.Api.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace BookIt.Api.FunctionalTests.Users;

public class RegisterUserTests : BaseFunctionalTests
{
    public RegisterUserTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenRequestIsValid()
    {
        var request = new RegisterUserRequest("create@test.de", "first", "last", "123456789");
        
        var response = await HttpClient.PostAsJsonAsync("api/v1/users/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Theory]
    [InlineData("", "first", "last", "123456")]
    [InlineData("briel.com", "first", "last", "12345")]
    [InlineData("@briel.com", "first", "last", "12345")]
    [InlineData("briel@", "first", "last", "12345")]
    [InlineData("briel@mail.com", "", "last", "12345")]
    [InlineData("briel@mail.com", "first", "", "12345")]
    [InlineData("briel@mail.com", "first", "last", "")]
    [InlineData("briel@mail.com", "first", "last", "p")]
    [InlineData("briel@mail.com", "first", "last", "pa")]
    [InlineData("briel@mail.com", "first", "last", "pas")]
    [InlineData("briel@mail.com", "first", "last", "pass")]
    public async Task Register_ShouldReturnBadRequest_WhenRequestIsInvalid(
        string email, 
        string firstName,
        string lastName,
        string password)
    {
        var request = new RegisterUserRequest(email, firstName, lastName, password);
        
        var response = await HttpClient.PostAsJsonAsync("api/v1/users/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}