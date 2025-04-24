using BookIt.Domain.Bookings;
using BookIt.Domain.Shared;
using BookIt.Domain.UnitTests.Apartments;
using FluentAssertions;

namespace BookIt.Domain.UnitTests.Bookings;

public class PricingServiceTests
{
    [Fact]
    public void CalculatePrice_ShouldReturnExpectedPrice()
    {
        // Arrange
        var price = new Money(10.0m, Currency.Eur);
        var period = DateRange.Create(new DateOnly(2025, 5, 1), new DateOnly(2025, 5, 10));
        var expectedTotalPrice = new Money(price.Amount * period.LengthInDays, price.Currency);

        var apartment = ApartmentData.Create(price);
        var pricingService = new PricingService();
        
        // Act
        var pricingDetails = pricingService.CalculatePrice(apartment, period);
        
        // Assert
        pricingDetails.TotalPrice.Should().Be(expectedTotalPrice);
    }
    
    [Fact]
    public void CalculatePrice_ShouldReturnExpectedPrice_WhenCleaningFeeIsIncluded()
    {
        var price = new Money(10.0m, Currency.Eur);
        var cleaningFee = new Money(100.0m, Currency.Eur);
        var period = DateRange.Create(new DateOnly(2025, 5, 1), new DateOnly(2025, 5, 10));
        var expectedTotalPrice = new Money(price.Amount * period.LengthInDays + cleaningFee.Amount, price.Currency);

        var apartment = ApartmentData.Create(price, cleaningFee);
        var pricingService = new PricingService();
        
        // Act
        var pricingDetails = pricingService.CalculatePrice(apartment, period);
        
        // Assert
        pricingDetails.TotalPrice.Should().Be(expectedTotalPrice);
    }
}