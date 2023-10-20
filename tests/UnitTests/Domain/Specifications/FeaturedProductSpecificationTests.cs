using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.Specifications;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Specifications;

public class FeaturedProductSpecificationTests
{
    [Theory]
    [InlineAutoMoqData("ProductA")]
    [InlineAutoMoqData("ProductB")]
    [InlineAutoMoqData("ProductC")]
    public void Should_ReturnTrue_When_ProductMatchesFeaturedName(
        string productName, 
        PositiveMoney money,
        FeaturedProductSpecification sut)
    {
        //Arrange
        var product = Product.AddToProductCatalog(new ProductName(productName), money, money, money);
        
        //Act
        var actual = sut.IsSatisfied(product);
        
        //Assert
        actual.Should().BeTrue();
    }
    
    [Theory]
    [AutoMoqData]
    public void Should_ReturnFalse_When_ProductMatchesFeaturedName(
        Product product,
        FeaturedProductSpecification sut)
    {
        //Act
        var actual = sut.IsSatisfied(product);
        
        //Assert
        actual.Should().BeFalse();
    }
}