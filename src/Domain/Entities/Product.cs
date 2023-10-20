using System;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.Domain.Entities;

public class Product : AggregateRoot<Guid>
{
    private Product(Guid id, ProductName name, PositiveMoney price, PositiveMoney tax, PositiveMoney shipping)
    {
        Id = id;
        Name = name;
        Price = price;
        Tax = tax;
        Shipping = shipping;
    }
    
    public static Product AddToProductCatalog(ProductName name, PositiveMoney price, PositiveMoney tax, PositiveMoney shipping)
    {
        return new Product(Guid.NewGuid(), name, price, tax, shipping);
    }

    public ProductName Name { get; private set; }
    public PositiveMoney Price { get; private set; }
    public PositiveMoney Tax { get; private set; }
    public PositiveMoney Shipping { get; private set; }
}