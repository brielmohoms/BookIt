using Microsoft.EntityFrameworkCore;

namespace BookIt.Domain.Users;

[Owned]
public record Email(
    string Value);