using BookIt.Application.Abstractions.Authentication;
using BookIt.Application.Messaging;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Users;

namespace BookIt.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync( // responsible to send our credentials to the keycloak api and receiving an access token
            request.Email, 
            request.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }
        
        return new AccessTokenResponse(result.Value);
    }
}