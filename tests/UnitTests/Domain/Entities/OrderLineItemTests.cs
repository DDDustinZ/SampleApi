using System;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class OrderLineItemTests
{
    [Theory]
    [AutoMoqData]
    public void Should_CreateNewShoppingCartLineItem(Guid orderId, ShoppingCartLineItem shoppingCartLineItem)
    {
        //Act
        var sut = OrderLineItem.ProcessLineItem(orderId, shoppingCartLineItem);

        //Assert
        sut.Id.Should().NotBeEmpty();
        sut.OrderId.Should().Be(orderId);
        sut.Product.Should().Be(shoppingCartLineItem.Product);
        sut.Quantity.Should().Be(new Quantity(1));
    }
}