using Microsoft.AspNetCore.Authorization;

namespace BookIt.Infrastructure.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) 
        : base(permission)
    {
    }
}