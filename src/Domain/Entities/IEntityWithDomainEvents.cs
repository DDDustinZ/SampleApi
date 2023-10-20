using System.Collections.Generic;
using CompanyName.SampleApi.Domain.DomainEvent;

namespace CompanyName.SampleApi.Domain.Entities;

public interface IEntityWithDomainEvents
{
    IReadOnlyList<BaseDomainEvent> DomainEvents { get; }
    void ClearEvents();
}