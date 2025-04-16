using BookIt.Application.Abstractions.Messaging;

namespace BookIt.Application.Bookings.ConfirmBooking;

public sealed record ConfirmBookingCommand(
    Guid BookingId) : ICommand;