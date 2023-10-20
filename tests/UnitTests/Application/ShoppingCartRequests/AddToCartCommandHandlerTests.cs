using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Application.ShoppingCartRequests;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Application.ShoppingCartRequests;

public class AddToCartCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task Should_SaveNewProductToShoppingCart_When_AddingToCart(
        [Frozen] Mock<ISalesUnitOfWork> salesUnitOfWork, 
        [Frozen] ShoppingCart shoppingCart, 
        [Frozen] Product productToAdd,
        AddToCartCommand inputModel,
        AddToCartCommandHandler sut)
    {
        //Arrange
        var initialCount = shoppingCart.LineItems.Count;
    
        //Act
        await sut.Handle(inputModel, CancellationToken.None);
    
        //Assert
        shoppingCart.LineItems.Count.Should().Be(initialCount + 1);
        shoppingCart.LineItems.Should().Contain(x => x.Product == productToAdd);
        salesUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }
}