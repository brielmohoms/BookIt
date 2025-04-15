using BookIt.Domain.Users;

namespace BookIt.Infrastructure.Authorization;

public sealed class UserRolesResponse
{
    public Guid UserId { get; init; }

    public List<Role> Roles { get; init; } = [];
}