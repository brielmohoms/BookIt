using System.Text.Json.Serialization;

namespace BookIt.Infrastructure.Authentication.Models;

public sealed class AuthorizationToken // used to authenticate to the keycloak admin api
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
}