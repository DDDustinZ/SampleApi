using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.SampleApi.Domain.DomainEvent;

namespace CompanyName.SampleApi.Domain.Entities;

public abstract class Entity<TId> : IEntityWithDomainEvents, IEquatable<Entity<TId>> where TId : IEquatable<TId>
{
    private readonly List<BaseDomainEvent> _domainEvents = new();
        
    public TId Id { get; protected set; }

    [NotMapped]
    public IReadOnlyList<BaseDomainEvent> DomainEvents => _domainEvents;
        
    protected void AddDomainEvent(BaseDomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    public virtual bool Equals(Entity<TId>? other)
    {
        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return !IsTransient() && !other.IsTransient() && Id.Equals(other.Id);
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity<TId>;
        return Equals(compareTo);
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return (GetType().ToString() + Id).GetHashCode();
    }

    public static bool operator ==(Entity<TId>? x, Entity<TId>? y)
    {
        if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
            return true;

        return !ReferenceEquals(x, null) && x.Equals(y);
    }

    public static bool operator !=(Entity<TId> x, Entity<TId> y)
    {
        return !(x == y);
    }

    public bool IsTransient()
    {
        return Id.Equals(default);
    }
}