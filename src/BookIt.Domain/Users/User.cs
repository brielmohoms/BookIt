using BookIt.Domain.Abstractions;
using BookIt.Domain.Users.Events;

namespace BookIt.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Role> _roles = new(); // allow to encapsulate the roles for users. So they can be added or removed using the user entity
    
    private User(Guid userId, FirstName firstName, LastName lastName, Email email) 
        : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private User()
    {
    }
    
    public FirstName FirstName { get; private set; }
    
    public LastName LastName { get; private set; }
    
    public Email Email { get; private set; }

    public string IdentityId { get; private set; } = string.Empty;
    
    public IReadOnlyCollection<Role> Roles => _roles.ToList();

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        
        user._roles.Add(Role.Registered);
        
        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}