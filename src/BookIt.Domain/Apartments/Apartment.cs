using BookIt.Domain.Abstractions;

namespace BookIt.Domain.Apartments;

public sealed class Apartment : Entity // sealed bc it is not going to be inherited
{
    public Apartment(Guid id) 
        : base(id)
    {
    }
    
    public Name Name { get; private set; }
    
    public Address Address { get; private set; }
    
    public Description Description { get; private set; }
    
    public Money Price { get; private set; }
    
    public Money CleaningFee { get; private set; }
    
    public DateTime LastBookedOnUtc { get; internal set; }

    public List<Amenity> Amenities { get; private set; } = new();
    
}