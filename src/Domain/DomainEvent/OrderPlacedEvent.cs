using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Domain.DomainEvent;

public class OrderPlacedEvent : BaseDomainEvent
{
    public Order Order { get; }

    public OrderPlacedEvent(Order order)
    {
        Order = order;
    }
}