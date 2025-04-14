using BookIt.Domain.Users;

namespace BookIt.Infrastructure.Authorization;

public sealed class UserRolesResponse
{
    public Guid Id { get; init; }

    public List<Role> Roles { get; init; } = [];
}