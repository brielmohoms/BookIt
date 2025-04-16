using BookIt.Application.Abstractions.Clock;
using BookIt.Application.Abstractions.Messaging;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Bookings;

namespace BookIt.Application.Bookings.ConfirmBooking;

internal sealed class ConfirmBookingCommandHandler : ICommandHandler<ConfirmBookingCommand>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmBookingCommandHandler(
        IDateTimeProvider dateTimeProvider, 
        IBookingRepository bookingRepository, 
        IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(
        ConfirmBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var result = booking.ConfirmBooking(_dateTimeProvider.Now);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}