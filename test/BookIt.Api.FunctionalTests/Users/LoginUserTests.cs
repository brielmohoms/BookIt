using System.Net;
using System.Net.Http.Json;
using BookIt.Api.Controllers.Users;
using BookIt.Api.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace BookIt.Api.FunctionalTests.Users;

public class LoginUserTests : BaseFunctionalTests
{
    private const string Email = "chris@webmail.de";
    private const string Password = "justARandomPassword";
    
    public LoginUserTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserDoesNotExist()
    {
        var request = new LoginUserRequest(Email, Password);
        
        var response = await HttpClient.PostAsJsonAsync("api/v1/users/login", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Login_ShouldReturnOk_WhenUserExists()
    {
        var registerRequest = new RegisterUserRequest(Email, "first", "last", Password);
        await HttpClient.PostAsJsonAsync("api/v1/users/register", registerRequest);
        
        var request = new LoginUserRequest(Email, Password);
        
        var response = await HttpClient.PostAsJsonAsync("api/v1/users/login", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}