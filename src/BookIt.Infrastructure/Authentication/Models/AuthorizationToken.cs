using System.Text.Json.Serialization;

namespace BookIt.Infrastructure.Models;

public sealed class AuthorizationToken // used to authenticate to the keycloak admin api
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}