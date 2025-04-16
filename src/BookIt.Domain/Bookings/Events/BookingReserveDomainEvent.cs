using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Bookings.Events;

public sealed record BookingReserveDomainEvent(Guid BookingId) : IDomainEvent;