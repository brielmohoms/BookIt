using BookIt.Application.Abstractions.Messaging;

namespace BookIt.Application.Bookings.CancelBooking;

public record CancelBookingCommand(Guid BookingId) : ICommand;