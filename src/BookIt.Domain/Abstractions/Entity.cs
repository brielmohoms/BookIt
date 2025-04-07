namespace BookIt.Domain.Abstractions;

public abstract class Entity // We can't create an entity instance but we can only inherit from it
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected Entity(Guid id)
    {
        Id = id;
    }
    
    protected Entity()
    {
    }
    
    public Guid Id { get; init; } // init setter, once we define it, the id is set for life

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}