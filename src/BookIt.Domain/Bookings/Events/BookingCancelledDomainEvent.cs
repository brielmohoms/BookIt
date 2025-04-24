using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Bookings.Events;

public sealed record BookingCancelledDomainEvent(Guid bookingId) : IDomainEvent;