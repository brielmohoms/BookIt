using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Users.Events;

public record BookingConfirmedDomainEvent(Guid bookingId) : IDomainEvent;