using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BookIt.Api.FunctionalTests.Infrastructure;
using BookIt.Application.Users.GetLoggedInUser;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookIt.Api.FunctionalTests.Users;

public class GetLoggedInUserTests : BaseFunctionalTests
{
    public GetLoggedInUserTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_ShouldReturnUnauthorized_WhenAccessTokenIsMissing()
    {
        var response = await HttpClient.GetAsync("api/v1/users/me");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_ShouldReturnUser_WhenAccessTokenIsValid()
    {
        var accessToken = await GetAccessToken();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            accessToken);
        
        var user = await HttpClient.GetFromJsonAsync<UserResponse>($"api/v1/users/me");
        
        user.Should().NotBeNull();
    }
}