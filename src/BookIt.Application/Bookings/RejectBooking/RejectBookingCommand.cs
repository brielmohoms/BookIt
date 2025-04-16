using BookIt.Application.Abstractions.Messaging;

namespace BookIt.Application.Bookings.RejectBooking;

public sealed record RejectBookingCommand(
    Guid BookingId) : ICommand;