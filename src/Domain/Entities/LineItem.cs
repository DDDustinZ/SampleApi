using System;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.Domain.Entities;

public abstract class LineItem : Entity<Guid>
{
    protected LineItem()
    {
    }

    protected LineItem(Product product, Quantity quantity)
    {
        ProductId = product.Id;
        Product = product;
        Quantity = quantity;
    }
    
    public Guid ProductId { get; protected set; }
    public Product Product { get; protected set; } = null!;
    public Quantity Quantity { get; protected set; } = null!;
    public PositiveMoney LineSubTotal => Product.Price * Quantity;

    public void UpdateQuantity(Quantity newQuantity)
    {
        Quantity = newQuantity;
    }
}