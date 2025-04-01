namespace BookIt.Domain.Apartments;

public interface IApartmentRepository
{
    Task<Apartment> GetApartmentByIdAsync(Guid id, CancellationToken cancellationToken = default);
}