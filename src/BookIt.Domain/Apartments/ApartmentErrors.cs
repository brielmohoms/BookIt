using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Apartments;

public static class ApartmentErrors
{
    public static Error NotFound = new(
        "Apartment.Found",
        "Apartment not found");
    
}