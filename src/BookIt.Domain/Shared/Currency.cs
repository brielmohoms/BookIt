namespace BookIt.Domain.Shared;

public record Currency
{
    internal static readonly Currency None = new("");  // internal keyword allowing to hide the field from outside of the domain assembly 
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    
    private Currency(string code) => Code = code;
    
    public string Code { get; init; }

    public static Currency FromCode(string code)
    {
        return Everything.FirstOrDefault(c => c.Code == code) ??
               throw new ApplicationException("Unknown currency code");
    }

    public static readonly IReadOnlyCollection<Currency> Everything = new[] 
    {
        Usd,
        Eur
    };
}