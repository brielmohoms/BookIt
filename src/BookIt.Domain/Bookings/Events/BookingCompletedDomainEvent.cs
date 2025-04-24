using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Bookings.Events;

public sealed record BookingCompletedDomainEvent(Guid bookingId) : IDomainEvent;