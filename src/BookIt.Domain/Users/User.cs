using BookIt.Domain.Abstractions;
using BookIt.Domain.Users.Events;

namespace BookIt.Domain.Users;

public sealed class User : Entity
{
    private User(Guid userId, FirstName firstName, LastName lastName, Email email) 
        : base(userId)
    {
    }
    
    public FirstName FirstName { get; private set; }
    
    public LastName LastName { get; private set; }
    
    public Email Email { get; private set; }

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        
        return user;
    }
}