using System.Threading.Tasks;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Infrastructure.Data.Repositories;
using CompanyName.SampleApi.IntegrationTests.TestFixture;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompanyName.SampleApi.IntegrationTests.Data;

public class ShoppingCartRepositoryTests : RepositoryTestBase
{
    private readonly ShoppingCartRepository _sut;

    public ShoppingCartRepositoryTests(DatabaseTestFixture fixture) : base(fixture)
    {
        _sut = fixture.Services.GetRequiredService<ShoppingCartRepository>();
    }
    
    [Theory]
    [AutoMoqData]
    public async Task Should_InsertShoppingCart(ShoppingCart expected)
    {
        //Act
        _sut.Insert(expected);
        await ScopedUnitOfWork.SaveChangesAsync();
        
        //Assert
        var actual = await SalesDbContext.ShoppingCarts.FindAsync(expected.Id);
        ReferenceEquals(actual, expected).Should().BeFalse();
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [AutoMoqData]
    public async Task Should_GetShoppingCartById(ShoppingCart expected)
    {
        //Arrange
        SalesDbContext.ShoppingCarts.Add(expected);
        await SalesDbContext.SaveChangesAsync();
        
        //Act
        var actual = await _sut.GetByIdAsync(expected.Id);
        
        //Assert
        ReferenceEquals(actual, expected).Should().BeFalse();
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [AutoMoqData]
    public async Task Should_DeleteShoppingCart(ShoppingCart expected)
    {
        //Arrange
        SalesDbContext.ShoppingCarts.Add(expected);
        await SalesDbContext.SaveChangesAsync();
        
        //Act
        _sut.Delete(expected);
        await ScopedUnitOfWork.SaveChangesAsync();
        
        //Assert
        var allCarts = await SalesDbContext.ShoppingCarts.ToListAsync();
        allCarts.Should().BeEmpty();
    }
}