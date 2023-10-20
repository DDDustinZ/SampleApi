using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Domain.Entities;
using MediatR;

namespace CompanyName.SampleApi.Application.OrderRequests;

public class PlaceOrderCommand : IRequest
{
    public Guid ShoppingCartId { get; set; }
}
    
public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand>
{
    private readonly ISalesUnitOfWork _salesUnitOfWork;

    public PlaceOrderCommandHandler(ISalesUnitOfWork salesUnitOfWork)
    {
        _salesUnitOfWork = salesUnitOfWork;
    }

    public async Task Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _salesUnitOfWork.ShoppingCartRepository.GetByIdAsync(request.ShoppingCartId, cancellationToken);
        var order = Order.PlaceOrder(shoppingCart);

        _salesUnitOfWork.OrderRepository.Insert(order);
        await _salesUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}