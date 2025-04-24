using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Bookings.Events;

public sealed record BookingConfirmedDomainEvent(Guid bookingId) : IDomainEvent;