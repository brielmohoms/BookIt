using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Bookings.Events;

public sealed record BookingRejectDomainEvent(Guid bookingId) : IDomainEvent;