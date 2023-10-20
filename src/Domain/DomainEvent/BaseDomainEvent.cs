using System;

namespace CompanyName.SampleApi.Domain.DomainEvent;

public abstract class BaseDomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}