using System;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.Domain.Entities;

public class OrderLineItem : LineItem
{
    // ReSharper disable once UnusedMember.Local - Used by EF
    private OrderLineItem()
    {
    }
    
    private OrderLineItem(Guid orderId, Product product, Quantity quantity) : base(product, quantity)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
    }

    public static OrderLineItem ProcessLineItem(Guid orderId, ShoppingCartLineItem shoppingCartLineItem)
    {
        return new OrderLineItem(orderId, shoppingCartLineItem.Product, shoppingCartLineItem.Quantity);
    }
    
    public Guid OrderId { get; }
}