using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Application.OrderRequests;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.UnitTests.TestFramework;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Application.OrderRequests;

public class PlaceOrderCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task Should_PlaceOrderAndPublishEvent_When_PlacingAnOrder(
        [Frozen] ShoppingCart shoppingCart,
        [Frozen] Mock<IOrderRepository> orderRepository,
        [Frozen] Mock<ISalesUnitOfWork> salesUnitOfWork,
        PlaceOrderCommandHandler sut)
    {
        //Arrange
        var expectedOrder = Order.PlaceOrder(shoppingCart);
        var config = new ComparisonConfig { TypesToIgnore = new List<Type> { typeof(Guid) }, MaxMillisecondsDateDifference = 1000 };
        
        //Act
        await sut.Handle(new PlaceOrderCommand {ShoppingCartId = shoppingCart.Id}, CancellationToken.None);

        //Assert
        orderRepository.Verify(x => x.Insert(It.Is<Order>(y => y.VerifyDeepEqual(expectedOrder, config))));
        salesUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }
}