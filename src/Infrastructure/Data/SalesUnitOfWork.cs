using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Infrastructure.Data.Repositories;
using MediatR;

namespace CompanyName.SampleApi.Infrastructure.Data;

public class SalesUnitOfWork : UnitOfWork, ISalesUnitOfWork
{
    private readonly SalesDbContext _salesDbContext;
    private readonly IMediator _mediator;

    public SalesUnitOfWork(SalesDbContext salesDbContext, IMediator mediator)
        : base(salesDbContext, mediator)
    {
        _salesDbContext = salesDbContext;
        _mediator = mediator;
    }

    public IProductRepository ProductRepository => new ProductRepository(_salesDbContext);
    public IOrderRepository OrderRepository => new OrderRepository(_salesDbContext);
    public IShoppingCartRepository ShoppingCartRepository => new ShoppingCartRepository(_salesDbContext);
}