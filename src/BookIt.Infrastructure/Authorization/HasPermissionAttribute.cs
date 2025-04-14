using Microsoft.AspNetCore.Authorization;

namespace BookIt.Infrastructure.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) 
        : base(permission)
    {
    }
}