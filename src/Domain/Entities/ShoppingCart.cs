using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.Domain.Entities;

public class ShoppingCart : AggregateRoot<Guid>
{
    private List<ShoppingCartLineItem> _lineItems;

    private ShoppingCart()
    {
    }

    public static ShoppingCart StartShopping(Guid userId)
    {
        return new ShoppingCart
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            _lineItems = new List<ShoppingCartLineItem>(),
            SubTotal = 0,
            TotalTax = 0,
            TotalShipping = 0,
            Total = 0
        };
    }

    public Guid UserId { get; private set; }
    public IReadOnlyList<ShoppingCartLineItem> LineItems => _lineItems;
    public PositiveMoney SubTotal { get; private set; }
    public PositiveMoney TotalTax { get; private set; }
    public PositiveMoney TotalShipping { get; private set; }
    public PositiveMoney Total { get; private set; }

    public void AddProduct(Product product)
    {
        var existingProductLineItem = _lineItems.SingleOrDefault(x => x.Product == product);

        if (existingProductLineItem == null)
        {
            _lineItems.Add(ShoppingCartLineItem.AddNewLineItem(Id, product));
        }
        else
        {
            existingProductLineItem.UpdateQuantity(existingProductLineItem.Quantity + 1);
        }

        CalculateTotals();
    }

    private void CalculateTotals()
    {
        SubTotal = 0;
        TotalTax = 0;
        TotalShipping = 0;

        foreach (var lineItem in _lineItems)
        {
            SubTotal += lineItem.LineSubTotal;
            TotalTax += lineItem.Product.Tax;
            TotalShipping += lineItem.Product.Shipping;
        }

        Total = SubTotal + TotalTax + TotalShipping;
    }
}