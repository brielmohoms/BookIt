using BookIt.Application.Abstractions.Clock;
using BookIt.Application.Abstractions.Messaging;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Bookings;

namespace BookIt.Application.Bookings.CompleteBooking;

internal sealed class CompleteBookingCommandHandler : ICommandHandler<CompleteBookingCommand>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteBookingCommandHandler(
        IDateTimeProvider dateTimeProvider, 
        IBookingRepository bookingRepository, 
        IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(CompleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var result = booking.CompleteBooking(_dateTimeProvider.Now);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}