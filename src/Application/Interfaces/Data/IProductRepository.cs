using System;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface IProductRepository : IAsyncGettableRepository<Product, Guid>
{
}