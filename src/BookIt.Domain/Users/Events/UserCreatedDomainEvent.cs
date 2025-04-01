using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;