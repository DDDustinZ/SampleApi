using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class ProductTests
{
    [Theory]
    [AutoMoqData]
    public void Should_SetProperties_When_CallingAddNewLineItem(
        ProductName productName, 
        PositiveMoney itemPrice, 
        PositiveMoney tax, 
        PositiveMoney shipping)
    {
        //Act
        var product = Product.AddToProductCatalog(productName, itemPrice, tax, shipping);

        //Assert
        product.Name.Should().Be(productName);
        product.Price.Should().Be(itemPrice);
        product.Tax.Should().Be(tax);
        product.Shipping.Should().Be(shipping);
    }
}