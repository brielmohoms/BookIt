using System.Security.Claims;

namespace BookIt.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
               throw new ApplicationException("user identity is unavailable");
    }
}