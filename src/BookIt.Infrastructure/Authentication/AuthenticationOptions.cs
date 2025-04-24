namespace BookIt.Infrastructure.Authentication;

public sealed class AuthenticationOptions  //Represent the authentication options
                                           // The values are obtained from the appsettings.json file
{
    public string Audience { get; init; } = string.Empty;
    
    public string MetadataUrl { get; set; } = string.Empty;
    
    public bool RequireHttpsMetadata { get; init; }
    
    public string Issuer { get; set; } = string.Empty;
}