namespace Blog.Domain.Abstraction;

public abstract class Entity<TId> : IHasDomainEvents 
    where TId : ValueObject
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    public TId Id { get; private set; }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
#pragma warning disable CS8618
    protected Entity()
    {
    }
#pragma warning restore CS8618
}