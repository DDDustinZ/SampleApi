using System;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Infrastructure.Data.Repositories;

public class ProductRepository : AsyncRepository<Product, Guid>, IProductRepository
{
    public ProductRepository(SalesDbContext dbContext) : base(dbContext)
    {
    }
}