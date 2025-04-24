using BookIt.Domain.Apartments;
using BookIt.Domain.Shared;

namespace BookIt.Application.UnitTests.Apartments;

internal static class ApartmentData
{
    public static Apartment Create() => new(
        Guid.NewGuid(),
        new Name("Test apartment"),
        new Address("Country", "State", "Zipcode", "City", "Street"),
        new Description("Test description"),
        new Money(100.0m, Currency.Eur),
        Money.Zero(),
        []);

} 