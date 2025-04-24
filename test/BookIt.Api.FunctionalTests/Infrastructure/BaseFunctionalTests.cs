using System.Net.Http.Json;
using BookIt.Api.Controllers.Users;
using BookIt.Api.FunctionalTests.Users;
using BookIt.Application.Users.LoginUser;

namespace BookIt.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTests : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient;

    protected BaseFunctionalTests(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }

    protected async Task<string> GetAccessToken() // to obtain the access token
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(
            "api/v1/users/login",
            new LoginUserRequest(
                UserData.RegisterTestUserRequest.Email,
                UserData.RegisterTestUserRequest.Password));
        
        var accessTokenResponse = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
        
        return accessTokenResponse!.AccessToken;
    }
}