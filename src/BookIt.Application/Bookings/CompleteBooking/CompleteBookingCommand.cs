using BookIt.Application.Abstractions.Messaging;

namespace BookIt.Application.Bookings.CompleteBooking;

public record CompleteBookingCommand(
    Guid BookingId) : ICommand;