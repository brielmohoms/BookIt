using BookIt.Application.Abstractions.Email;
using BookIt.Domain.Bookings;
using BookIt.Domain.Users;
using BookIt.Domain.Users.Events;
using MediatR;

namespace BookIt.Application.Bookings.ReserveBooking;

internal sealed class BookingReserveDomainEventHandler : INotificationHandler<BookingReserveDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public BookingReserveDomainEventHandler(
        IBookingRepository bookingRepository, 
        IUserRepository userRepository, 
        IEmailService emailService)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(BookingReserveDomainEvent notification, CancellationToken cancellationToken)
    { 
        var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking is null)
        {
            return;
        }
        
        var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendEmailAsync(user.Email,
            "Booking reserved!",
            "You have 10 minutes to confirm this booking");
    }
}