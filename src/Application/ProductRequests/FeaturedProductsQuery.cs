using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.SampleApi.Application.ProductRequests;

public record FeaturedProductsQuery : IRequest<IEnumerable<FeaturedProductsResponse>>
{
}
    
public class FeaturedProductsQueryHandler : IRequestHandler<FeaturedProductsQuery, IEnumerable<FeaturedProductsResponse>>
{
    private readonly ISalesQueryContext _salesQueryContext;

    public FeaturedProductsQueryHandler(ISalesQueryContext salesQueryContext)
    {
        _salesQueryContext = salesQueryContext;
    }
        
    public async Task<IEnumerable<FeaturedProductsResponse>> Handle(FeaturedProductsQuery request, CancellationToken cancellationToken)
    {
        var featuredProductSpec = new FeaturedProductSpecification();

        return await _salesQueryContext.Products
            .Where(featuredProductSpec.Expression)
            .Select(x => new FeaturedProductsResponse
            {
                Name = x.Name,
                Price = x.Price
            }) 
            .ToListAsync(cancellationToken);
    }
}