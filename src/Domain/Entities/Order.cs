using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.SampleApi.Domain.DomainEvent;
using CompanyName.SampleApi.Domain.Exceptions;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.Domain.Entities;

public class Order : AggregateRoot<Guid>
{
    private List<OrderLineItem> _lineItems;

    private Order()
    {
    }

    public static Order PlaceOrder(ShoppingCart shoppingCart)
    {
        if (shoppingCart.LineItems.Count < 1)
            throw new CannotPlaceOrderWithNoLineItemsException();

        var orderId = Guid.NewGuid();
        var order = new Order
        {
            Id = orderId,
            UserId = shoppingCart.UserId,
            _lineItems = shoppingCart.LineItems.Select(x => OrderLineItem.ProcessLineItem(orderId, x)).ToList(),
            SubTotal = shoppingCart.SubTotal,
            TotalTax = shoppingCart.TotalTax,
            TotalShipping = shoppingCart.TotalShipping,
            Total = shoppingCart.Total
        };

        order.AddDomainEvent(new OrderPlacedEvent(order));

        return order;
    }

    public Guid UserId { get; set; }
    public IReadOnlyList<OrderLineItem> LineItems => _lineItems;
    public PositiveMoney SubTotal { get; private set; }
    public PositiveMoney TotalTax { get; private set; }
    public PositiveMoney TotalShipping { get; private set; }
    public PositiveMoney Total { get; private set; }
}