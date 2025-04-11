using BookIt.Application.Messaging;

namespace BookIt.Application.Users.LoginUser;

public sealed record LoginUserCommand(
    string Email, 
    string Password) : ICommand<AccessTokenResponse>;