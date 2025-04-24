using BookIt.Domain.Bookings;
using BookIt.Domain.Bookings.Events;
using BookIt.Domain.Shared;
using BookIt.Domain.UnitTests.Apartments;
using BookIt.Domain.UnitTests.Infrastructure;
using BookIt.Domain.UnitTests.Users;
using BookIt.Domain.Users;
using FluentAssertions;

namespace BookIt.Domain.UnitTests.Bookings;

public class BookingTests : BaseTest
{
    [Fact]
    public void Reserve_Should_RaiseBookingReservedDomainEvent()
    {
        // Arrange
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);
        
        var price = new Money(10.0m, Currency.Eur);
        var period = DateRange.Create(new DateOnly(2025, 5, 1), new DateOnly(2025, 5, 10));
        var apartment = ApartmentData.Create(price);
        var pricingService = new PricingService();
        
        // Act
        var booking = Booking.Reserve(apartment, user.Id, period, DateTime.Now, pricingService);
        
        // Assert
        var domainEvent = AssertDomainEventWasPublished<BookingReserveDomainEvent>(booking);
        
        domainEvent.BookingId.Should().Be(booking.Id);
    }
}