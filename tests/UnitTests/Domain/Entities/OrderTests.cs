using System;
using CompanyName.SampleApi.Domain.DomainEvent;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.Exceptions;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class OrderTests
{
    [Theory]
    [AutoMoqData]
    public void Should_ThrowException_When_NoLineItems(Guid userId)
    {
        //Act
        var act = () => Order.PlaceOrder(ShoppingCart.StartShopping(userId));

        //Assert
        act.Should().Throw<CannotPlaceOrderWithNoLineItemsException>();
    }
    
    [Theory]
    [AutoMoqData]
    public void Should_SetProperties_When_PlaceOrder(ShoppingCart shoppingCart)
    {
        //Act
        var sut = Order.PlaceOrder(shoppingCart);

        //Assert
        sut.UserId.Should().Be(shoppingCart.UserId);
        sut.LineItems.Should().Contain(x => x.Product == shoppingCart.LineItems[0].Product && x.Quantity == shoppingCart.LineItems[0].Quantity && shoppingCart.LineItems[0].LineSubTotal == x.LineSubTotal);
        sut.LineItems.Should().Contain(x => x.Product == shoppingCart.LineItems[1].Product && x.Quantity == shoppingCart.LineItems[1].Quantity && shoppingCart.LineItems[1].LineSubTotal == x.LineSubTotal);
        sut.LineItems.Should().Contain(x => x.Product == shoppingCart.LineItems[2].Product && x.Quantity == shoppingCart.LineItems[2].Quantity && shoppingCart.LineItems[2].LineSubTotal == x.LineSubTotal);
    }

    [Theory]
    [AutoMoqData]
    public void Should_CreateDomainEvent_When_PlaceOrder(ShoppingCart shoppingCart)
    {
        //Act
        var sut = Order.PlaceOrder(shoppingCart);

        //Assert
        sut.DomainEvents.Should().Contain(x => x is OrderPlacedEvent);
    }
}