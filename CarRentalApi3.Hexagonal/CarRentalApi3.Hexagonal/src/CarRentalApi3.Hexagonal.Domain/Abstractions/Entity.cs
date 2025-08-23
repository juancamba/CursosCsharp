using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi3.Hexagonal.Domain.Abstractions;

public abstract class Entity<TEntityId> : IEntity
{
    protected Entity()
    {

    }
    private readonly List<IDomainEvent> _domainEvents = new();
    private DateTime CreatedAt = DateTime.UtcNow;
    

    protected Entity(TEntityId id)
    {
        Id = id;
    }

    public TEntityId? Id { get; init; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
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
