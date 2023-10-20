using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class LineItemTests
{
    [Theory]
    [AutoMoqData]
    public void Should_UpdateQuantity_When_CallingUpdateQuantity(LineItem lineItem, Quantity newQuantity)
    {
        //Act
        lineItem.UpdateQuantity(newQuantity);

        //Assert
        lineItem.Quantity.Should().Be(newQuantity);
    }

    [Theory]
    [InlineAutoMoqData(1000, 5, 5000)]
    [InlineAutoMoqData(0, 5, 0)]
    [InlineAutoMoqData(111, 2, 222)]
    public void Should_MultiplyItemPriceAndQuantity_When_CallingLineSubTotal(int itemPricePennies, int quantity, int total, ProductName productName, PositiveMoney tax, PositiveMoney shipping)
    {
        //Arrange
        var lineItem = new TestLineItem(Product.AddToProductCatalog(productName, itemPricePennies, tax, shipping), quantity);

        //Act
        lineItem.UpdateQuantity(new Quantity(quantity));

        //Assert
        lineItem.LineSubTotal.Should().Be(new PositiveMoney(total));
    }
}

internal class TestLineItem : LineItem
{
    public TestLineItem(Product product, Quantity quantity) : base(product, quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}