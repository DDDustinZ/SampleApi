using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Application.Interfaces.HttpClients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.SampleApi.Application.ShoppingCartRequests;

public record GetShoppingCartQuery(Guid UserId, Guid ShoppingCartId) : IRequest<ShoppingCartResponse>
{
}

public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartResponse>
{
    private readonly IUserHttpClient _userHttpClient;
    private readonly ISalesQueryContext _salesQueryContext;

    public GetShoppingCartQueryHandler(IUserHttpClient userHttpClient, ISalesQueryContext salesQueryContext)
    {
        _userHttpClient = userHttpClient;
        _salesQueryContext = salesQueryContext;
    }
        
    public async Task<ShoppingCartResponse> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
    {
        var user = _userHttpClient.GetUser(request.UserId);
        var fullName = user.FirstName + " " + user.LastName;
        
        var query =
            from lineItem in _salesQueryContext.ShoppingCartLineItems
            join product in _salesQueryContext.Products on lineItem.ProductId equals product.Id
            where lineItem.ShoppingCartId == request.ShoppingCartId
            select new LineItemResponse(product.Name, product.Price, lineItem.Quantity);

        var lineItems = await query.ToListAsync(cancellationToken);
        
        return new ShoppingCartResponse(fullName, lineItems);
    }
}