using BookIt.Domain.Apartments;
using BookIt.Domain.Shared;

namespace BookIt.Domain.UnitTests.Apartments;

internal static class ApartmentData
{
    public static Apartment Create(Money price, Money? cleaningFee = null) => new(
        Guid.NewGuid(),
        new Name("Test apartment"),
        new Address("Country", "State", "Zipcode", "City", "Street"),
        new Description("Test description"),
        price,
        cleaningFee ?? Money.Zero(),
        []);

} 