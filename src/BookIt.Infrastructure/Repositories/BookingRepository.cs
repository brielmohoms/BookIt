using BookIt.Domain;
using BookIt.Domain.Apartments;
using BookIt.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace BookIt.Infrastructure.Repositories;

internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed
    };

    public BookingRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public void AddBooking(Booking booking)
    {
        DbContext.Set<Booking>().Add(booking);
    }

    public async Task<bool> IsOverlappingAsync(
        Apartment apartment,
        DateRange duration,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Booking>()
            .AnyAsync(
                booking => 
                    booking.ApartmentId == apartment.Id && 
                    booking.Duration.StartDate <= duration.EndDate &&
                    booking.Duration.EndDate >= duration.StartDate &&
                    ActiveBookingStatuses.Contains(booking.Status),
                    cancellationToken);
    }
}