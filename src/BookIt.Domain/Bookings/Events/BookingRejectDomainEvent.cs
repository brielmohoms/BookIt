using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Users.Events;

public record BookingRejectDomainEvent(Guid bookingId) : IDomainEvent;