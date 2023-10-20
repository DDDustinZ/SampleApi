using System;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class ShoppingCartLineItemTests
{
    [Theory]
    [AutoMoqData]
    public void Should_CreateNewShoppingCartLineItem(Guid shoppingCardId, Product product)
    {
        //Act
        var sut = ShoppingCartLineItem.AddNewLineItem(shoppingCardId, product);

        //Assert
        sut.Id.Should().NotBeEmpty();
        sut.ShoppingCartId.Should().Be(shoppingCardId);
        sut.Product.Should().Be(product);
        sut.Quantity.Should().Be(new Quantity(1));
    }
}