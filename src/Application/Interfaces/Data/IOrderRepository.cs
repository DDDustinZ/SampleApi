using System;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface IOrderRepository : IAsyncGettableRepository<Order, Guid>, IAsyncInsertableRepository<Order, Guid>
{
}