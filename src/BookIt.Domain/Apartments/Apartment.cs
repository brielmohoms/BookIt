namespace BookIt.Domain.Apartments;

public sealed class Apartment // sealed bc it is not going to be inherited
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Description { get; set; }
    
    public string Country { get; set; }
    
    public string State { get; set; }
    
    public string ZipCode { get; set; }

    public string City { get; set; }

    public string Street { get; set; }
    
    public decimal Price { get; set; }
    
    public string PriceCurrency { get; set; }
    
    public string CleaningFeeAmount { get; set; }
    
    public string CleaningFeeCurrency { get; set; }
    
    public DateTime LastBookedOnUtc { get; set; }
}