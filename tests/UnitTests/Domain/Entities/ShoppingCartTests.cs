using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class ShoppingCartTests
{
    [Theory]
    [AutoMoqData]
    public void Should_CreateNewEmptyShoppingCart_When_StartShopping(Guid userId)
    {
        //Act
        var sut = ShoppingCart.StartShopping(userId);

        //Assert
        sut.UserId.Should().Be(userId);
        sut.LineItems.Count.Should().Be(0);
        sut.SubTotal.Should().Be(new PositiveMoney(0));
        sut.TotalTax.Should().Be(new PositiveMoney(0));
        sut.TotalShipping.Should().Be(new PositiveMoney(0));
        sut.Total.Should().Be(new PositiveMoney(0));
    }

    [Theory]
    [AutoMoqData]
    public void Should_AddProductToProducts_When_AddProductIsCalled(Guid userId, Product product)
    {
        //Arrange
        var sut = ShoppingCart.StartShopping(userId);

        //Act
        sut.AddProduct(product);

        //Assert
        sut.LineItems[0].Product.Should().Be(product);
        sut.SubTotal.Should().Be(product.Price);
        sut.TotalTax.Should().Be(product.Tax);
        sut.TotalShipping.Should().Be(product.Shipping);
        sut.Total.Should().Be(product.Price + product.Tax + product.Shipping);
    }

    [Theory]
    [AutoMoqData]
    public void Should_CalculateCorrectTotals_When_CalculateTotals(Guid userId, List<Product> products)
    {
        //Arrange
        var sut = ShoppingCart.StartShopping(userId);

        //Act
        products.ForEach(x => sut.AddProduct(x));

        //Assert
        sut.SubTotal.Should().Be(new PositiveMoney(products.Sum(x => x.Price)));
        sut.TotalTax.Should().Be(new PositiveMoney(products.Sum(x => x.Tax)));
        sut.TotalShipping.Should().Be(new PositiveMoney(products.Sum(x => x.Shipping)));
        sut.Total.Should().Be(new PositiveMoney(sut.SubTotal + sut.TotalTax + sut.TotalShipping));
    }
}