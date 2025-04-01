using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Users.Events;

public record BookingCompletedDomainEvent(Guid bookingId) : IDomainEvent;