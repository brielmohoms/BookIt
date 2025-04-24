using BookIt.Api.Controllers.Users;

namespace BookIt.Api.FunctionalTests.Users;

internal static class UserData
{
    public static RegisterUserRequest RegisterTestUserRequest = 
        new("test@test.de", "test", "test", "password");
}
