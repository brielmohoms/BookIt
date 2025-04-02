using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Bookings;

public static class BookingErrors
{
    public static Error NotFound = new(
        "Booking.Found",
        "The Booking with the specified id was not found.");
    
    public static Error Overlap = new(
        "Booking.Overlap",
        "The current booking is overlapping with an existing booking.");

    public static Error NotReserved = new(
        "Booking.NotReserved",
        "The booking is not pending");
    
    public static Error NotConfirmed = new(
        "Booking.NotReserved",
        "The booking is not confirmed");
    
    public static Error AlreadyStarted = new(
        "Booking.AlreadyStarted",
        "The booking is already started");
}