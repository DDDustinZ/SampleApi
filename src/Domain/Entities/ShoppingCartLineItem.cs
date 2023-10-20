using System;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.Domain.Entities;

public class ShoppingCartLineItem : LineItem
{
    // ReSharper disable once UnusedMember.Local - Used by EF
    private ShoppingCartLineItem()
    {
    }

    private ShoppingCartLineItem(Guid shoppingCartId, Product product, Quantity quantity) : base(product, quantity)
    {
        Id = Guid.NewGuid();
        ShoppingCartId = shoppingCartId;
    }

    public static ShoppingCartLineItem AddNewLineItem(Guid shoppingCartId, Product product)
    {
        return new ShoppingCartLineItem(shoppingCartId, product, 1);
    }

    public Guid ShoppingCartId { get; }
}