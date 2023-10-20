using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.Specifications;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.Infrastructure.Data.Repositories;
using CompanyName.SampleApi.IntegrationTests.TestFixture;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompanyName.SampleApi.IntegrationTests.Data;

public class ProductRepositoryTests : RepositoryTestBase
{
    private readonly ProductRepository _sut;

    public ProductRepositoryTests(DatabaseTestFixture fixture) : base(fixture)
    {
        _sut = fixture.Services.GetRequiredService<ProductRepository>();
    }

    [Theory]
    [AutoMoqData]
    public async Task Should_InsertProduct(Product expected)
    {
        //Act
        _sut.Insert(expected);
        await ScopedUnitOfWork.SaveChangesAsync();
        
        //Assert
        var actual = await SalesDbContext.Products.FindAsync(expected.Id);
        ReferenceEquals(actual, expected).Should().BeFalse();
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [AutoMoqData]
    public async Task Should_GetProductById(Product expected)
    {
        //Arrange
        SalesDbContext.Products.Add(expected);
        await SalesDbContext.SaveChangesAsync();
        
        //Act
        var actual = await _sut.GetByIdAsync(expected.Id);
        
        //Assert
        ReferenceEquals(actual, expected).Should().BeFalse();
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [AutoMoqData]
    public async Task Should_DeleteProduct(Product expected)
    {
        //Arrange
        SalesDbContext.Products.Add(expected);
        await SalesDbContext.SaveChangesAsync();
        
        //Act
        _sut.Delete(expected);
        await ScopedUnitOfWork.SaveChangesAsync();
        
        //Assert
        var allProducts = await SalesDbContext.Products.ToListAsync();
        allProducts.Should().BeEmpty();
    }
    
    [Theory]
    [AutoMoqData]
    public async Task Should_GetFeaturedProducts_When_UsingSpecification(
        FeaturedProductSpecification spec, 
        PositiveMoney money,
        List<Product> randomProducts)
    {
        //Arrange
        var expected = new List<Product>
        {
            Product.AddToProductCatalog(new ProductName("ProductA"), money, money, money),
            Product.AddToProductCatalog(new ProductName("ProductB"), money, money, money),
            Product.AddToProductCatalog(new ProductName("ProductC"), money, money, money)
        };
        await SalesDbContext.Products.AddRangeAsync(expected);
        await SalesDbContext.Products.AddRangeAsync(randomProducts);
        await SalesDbContext.SaveChangesAsync();
        
        //Act
        var actual = await _sut.GetAllBySpecAsync(spec);
        
        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
}