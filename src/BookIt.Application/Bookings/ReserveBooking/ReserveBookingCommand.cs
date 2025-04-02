using BookIt.Application.Messaging;

namespace BookIt.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(
    Guid UserId,
    Guid ApartmentId,
    DateOnly StartDate,
    DateOnly EndDate) : ICommand<Guid>;