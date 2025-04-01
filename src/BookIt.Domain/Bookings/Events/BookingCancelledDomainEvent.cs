using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Users.Events;

public record BookingCancelledDomainEvent(Guid bookingId) : IDomainEvent;