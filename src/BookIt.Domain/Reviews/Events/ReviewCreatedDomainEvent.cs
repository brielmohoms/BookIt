using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;