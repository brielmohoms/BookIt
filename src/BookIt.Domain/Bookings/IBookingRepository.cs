using BookIt.Domain.Apartments;

namespace BookIt.Domain.Bookings;
public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken);
   
    Task<Booking> GetBookingByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void AddBooking(Booking booking);
}