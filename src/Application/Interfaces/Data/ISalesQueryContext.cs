using System.Linq;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface ISalesQueryContext
{
    IQueryable<Product> Products { get; }
    IQueryable<Order> Orders { get; }
    IQueryable<OrderLineItem> OrderLineItems { get; }
    IQueryable<ShoppingCart> ShoppingCarts { get; }
    IQueryable<ShoppingCartLineItem> ShoppingCartLineItems { get; }
}