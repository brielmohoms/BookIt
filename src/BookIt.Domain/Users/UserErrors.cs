using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "User not found");
    
    public static Error InvalidCredentials = new(
        "User.Credentials",
        "Invalid credentials");
}