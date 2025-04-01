using BookIt.Domain.Apartments;

namespace BookIt.Domain;

public record PricingDetails(
    Money PriceForPeriod,
    Money CleaningFee,
    Money AmenitiesUpCharge,
    Money TotalPrice);