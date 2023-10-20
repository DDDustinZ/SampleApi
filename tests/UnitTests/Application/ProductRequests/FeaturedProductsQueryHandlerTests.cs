using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Application.ProductRequests;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Application.ProductRequests;

public class FeaturedProductsQueryHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task Should_ReturnFeaturedProducts_When_GettingHomeViewModel(
        [Frozen] Mock<ISalesQueryContext> queryContextMock,
        Fixture fixture,
        FeaturedProductsQueryHandler sut)
    {
        //Arrange
        var expected = new List<FeaturedProductsResponse>
        {
            new() { Name = "ProductA", Price = 100 },
            new() { Name = "ProductB", Price = 200 },
            new() { Name = "ProductC", Price = 300 }
        };
        var products = fixture.CreateMany<Product>(10).ToList();
        products.AddRange(expected.Select(x => Product.AddToProductCatalog(x.Name, x.Price, x.Price, x.Price)));

        queryContextMock
            .Setup(x => x.Products)
            .Returns(products.AsQueryable());

        //Act
        var response = await sut.Handle(new FeaturedProductsQuery(), CancellationToken.None);

        //Assert
        response.Should().BeEquivalentTo(expected);
    }
}