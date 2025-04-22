using BookIt.Application.Abstractions.Caching;
using BookIt.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BookIt.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var cacheKey = $"auth:roles-{identityId}";
        
        var cacheRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey); // access data in the cache

        if (cacheRoles is not null) 
        {
            return cacheRoles; // return if present
        }
        
        var roles = await _dbContext.Set<User>() // else fetching the data from the database
            .Where(user => user.IdentityId == identityId)
            .Select(user => new UserRolesResponse
            { 
                UserId = user.Id,
                Roles = user.Roles.ToList()
            })
            .FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles); // caching it for subsequent request

        return roles;
    }
    
    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        var cacheKey = $"auth:permissions-{identityId}";
        
        var cachePermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey); 

        if (cachePermissions is not null) 
        {
            return cachePermissions; 
        }
        
        var permissions = await _dbContext.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .SelectMany(user => user.Roles.Select(role => role.UsersPermissions))
            .FirstAsync();

        var permissionsSet = permissions.Select(p => p.Name).ToHashSet();
        
        await _cacheService.SetAsync(cacheKey, permissionsSet);
        
        return permissionsSet;
    }
}