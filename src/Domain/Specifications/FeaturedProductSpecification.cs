using System.Collections.Generic;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.Domain.Specifications;

public class FeaturedProductSpecification : Specification<Product>
{
    private readonly List<ProductName> _featuredProductNames = new()
    {
        "ProductA",
        "ProductB",
        "ProductC"
    };
    
    public override bool IsSatisfied(Product product)
    {
        return _featuredProductNames.Contains(product.Name);
    }
}