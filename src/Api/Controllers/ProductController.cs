using CompanyName.SampleApi.Application.ProductRequests;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.SampleApi.Api.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<FeaturedProductsResponse>> GetProducts()
    {
        return await Mediator.Send(new FeaturedProductsQuery());
    }
}