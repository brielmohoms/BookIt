using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Users.Events;

public sealed record BookingReserveDomainEvent(Guid BookingId) : IDomainEvent;