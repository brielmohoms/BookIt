using Microsoft.EntityFrameworkCore;

namespace BookIt.Domain.Users;

[Owned]
public record FirstName(
    string Value);