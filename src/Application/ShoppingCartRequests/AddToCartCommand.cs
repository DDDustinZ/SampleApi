using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.SampleApi.Application.Interfaces.Data;
using MediatR;

namespace CompanyName.SampleApi.Application.ShoppingCartRequests;

public class AddToCartCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid ShoppingCartId { get; set; }
    public Guid ProductId { get; set; }
}

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
{
    private readonly ISalesUnitOfWork _salesUnitOfWork;

    public AddToCartCommandHandler(ISalesUnitOfWork salesUnitOfWork)
    {
        _salesUnitOfWork = salesUnitOfWork;
    }

    public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var product = await _salesUnitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);
        var shoppingCart = await _salesUnitOfWork.ShoppingCartRepository.GetByIdAsync(request.ShoppingCartId, cancellationToken);

        shoppingCart.AddProduct(product);

        await _salesUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}