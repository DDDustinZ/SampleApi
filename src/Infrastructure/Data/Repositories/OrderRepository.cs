using System;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Infrastructure.Data.Repositories;

public class OrderRepository : AsyncRepository<Order, Guid>, IOrderRepository
{
    public OrderRepository(SalesDbContext dbContext) : base(dbContext)
    {
    }
}