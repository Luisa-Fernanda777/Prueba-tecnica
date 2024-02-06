namespace Domain.Primitives;

public abstract class AggregateRoot{
    private readonly List<DomainEvent> _domaintEvents = new();
    
    public ICollection<DomainEvent> GetDomainEvents() => _domaintEvents;

    protected void Raise(DomainEvent domainEvent){
        _domaintEvents.Add(domainEvent);
    }
}