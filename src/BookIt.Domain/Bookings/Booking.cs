using BookIt.Domain.Abstractions;
using BookIt.Domain.Apartments;
using BookIt.Domain.Shared;
using BookIt.Domain.Users.Events;

namespace BookIt.Domain.Bookings;

public sealed class Booking : Entity
{
    private Booking(
        Guid id, 
        Guid apartmentId, 
        Guid userId,
        DateRange duration, 
        Money priceForPeriod, 
        Money cleaningFee, 
        Money amenitiesUpCharge,
        Money totalPrice, 
        BookingStatus status, 
        DateTime dateCreated) 
        : base(id)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
        Status = status;
        DateCreated = dateCreated;
    }

    private Booking()
    {
    }

    public Guid UserId { get; private set; }
    
    public Guid ApartmentId { get; private set; }
    
    public DateRange Duration { get; private set; }
    
    public Money PriceForPeriod { get; private set; }
    
    public Money CleaningFee { get; private set; }
    
    public Money TotalPrice { get; private set; }
    
    public Money AmenitiesUpCharge { get; private set; }
    
    public BookingStatus Status { get; private set; }
    
    public DateTime DateCreated { get; private set; }
    
    public DateTime DateConfirmed { get; private set; }
    
    public DateTime DateCompleted { get; private set; }
    
    public DateTime DateCanceled { get; private set; }
    
    public DateTime DateRejected { get; private set; }

    public static Booking Reserve(
        Apartment apartment,
        Guid userId,
        DateRange duration, 
        DateTime now, 
        PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(apartment, duration);
        
        var booking = new Booking(
            Guid.NewGuid(), 
            apartment.Id, 
            userId,
            duration, 
            pricingDetails.PriceForPeriod, 
            pricingDetails.CleaningFee, 
            pricingDetails.AmenitiesUpCharge,
            pricingDetails.TotalPrice, 
            BookingStatus.Reserved, 
            now);
        
        booking.RaiseDomainEvent(new BookingReserveDomainEvent(booking.Id));
        
        apartment.LastBookedOnUtc = now;
        
        return booking;
    }

    public Result ConfirmBooking(DateTime now)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }
        
        Status = BookingStatus.Confirmed;
        
        DateConfirmed = now;
        
        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result RejectBooking(DateTime now)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }
        
        Status = BookingStatus.Rejected;
        DateRejected = now;
        
        RaiseDomainEvent(new BookingRejectDomainEvent(Id));
        
        return Result.Success();
    }

    public Result CompleteBooking(DateTime now)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }
        
        Status = BookingStatus.Completed;
        DateCompleted = now;
        
        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result CancelBooking(DateTime now)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }
        
        var currentDate = DateOnly.FromDateTime(now);

        if (currentDate > Duration.StartDate)
        {
            return Result.Failure(BookingErrors.AlreadyStarted);
        }
        
        Status = BookingStatus.Cancelled;
        DateCanceled = now;
        
        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }
}