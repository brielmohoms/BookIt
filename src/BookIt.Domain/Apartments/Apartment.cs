using BookIt.Domain.Abstractions;
using BookIt.Domain.Shared;

namespace BookIt.Domain.Apartments;

public sealed class Apartment : Entity // sealed bc it is not going to be inherited
{
    private Apartment() 
    {
    }

    public Apartment(
        Guid id, 
        Name name, 
        Address address, 
        Description description, 
        Money price, 
        Money cleaningFee,
        List<Amenity> amenities) 
        : base(id)
    {
        Name = name;
        Address = address;
        Description = description;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }

    public Name Name { get; private set; }
    
    public Address Address { get; private set; }
    
    public Description Description { get; private set; }
    
    public Money Price { get; private set; }
    
    public Money CleaningFee { get; private set; }
    
    public DateTime LastBookedOnUtc { get; internal set; }

    public List<Amenity> Amenities { get; private set; } = new();
    
}