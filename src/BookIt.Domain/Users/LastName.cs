using Microsoft.EntityFrameworkCore;

namespace BookIt.Domain.Users;

[Owned]
public record LastName(
    string Value);