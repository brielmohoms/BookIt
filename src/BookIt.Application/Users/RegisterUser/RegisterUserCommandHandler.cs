using BookIt.Application.Abstractions.Authentication;
using BookIt.Application.Messaging;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Users;

namespace BookIt.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticationService _authenticationService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = User.Create( // creating a new user and parsing the values to the values objects
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email));
        
        var identityId = await _authenticationService.RegisterAsync( // the authentication service register a user with keycloak and give us back the identity id
            user, 
            request.Password, 
            cancellationToken);
        
        user.SetIdentityId(identityId);
        
        _userRepository.Add(user);
        
        await _unitOfWork.SaveChangesAsync();
        
        return user.Id;
    }
}