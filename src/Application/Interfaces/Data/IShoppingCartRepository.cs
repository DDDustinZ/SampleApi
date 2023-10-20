using System;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface IShoppingCartRepository : IAsyncGettableRepository<ShoppingCart, Guid>
{
}