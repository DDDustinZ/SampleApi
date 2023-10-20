using System;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Infrastructure.Data.Repositories;

public class ShoppingCartRepository : AsyncRepository<ShoppingCart, Guid>, IShoppingCartRepository
{
    public ShoppingCartRepository(SalesDbContext dbContext) : base(dbContext)
    {
    }
}