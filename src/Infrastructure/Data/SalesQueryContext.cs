using System.Linq;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Infrastructure.Data;

public class SalesQueryContext : ISalesQueryContext
{
    private readonly SalesDbContext _salesDbContext;

    public SalesQueryContext(SalesDbContext salesDbContext)
    {
        _salesDbContext = salesDbContext;
    }

    public IQueryable<Product> Products => _salesDbContext.Products;
    public IQueryable<Order> Orders => _salesDbContext.Orders;
    public IQueryable<OrderLineItem> OrderLineItems => _salesDbContext.OrderLineItems;
    public IQueryable<ShoppingCart> ShoppingCarts => _salesDbContext.ShoppingCarts;
    public IQueryable<ShoppingCartLineItem> ShoppingCartLineItems => _salesDbContext.ShoppingCartLineItems;
}