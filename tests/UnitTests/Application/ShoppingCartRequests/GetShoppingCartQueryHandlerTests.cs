using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Application.ShoppingCartRequests;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Application.ShoppingCartRequests;

public class GetShoppingCartQueryHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task Should_ReturnShoppingCartData_When_GettingShoppingCart(
        [Frozen] ShoppingCartResponse shoppingCartResponse,
        [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
        GetShoppingCartQuery request,      
        GetShoppingCartQueryHandler sut)
    {
        //Arrange
        // shoppingCartRepositoryMock
        //     .Setup(x => x.GetBySpecAsync(It.IsAny<ShoppingCartViewModelSpecification>(), default))
        //     .ReturnsAsync(shoppingCartResponse);
            
        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        response.Should().Be(shoppingCartResponse);
    }
}