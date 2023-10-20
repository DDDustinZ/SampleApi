using CompanyName.SampleApi.Application.ShoppingCartRequests;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.SampleApi.Api.Controllers;

[Route("shopping-cart")]
public class ShoppingCartController : ApiControllerBase
{
    [HttpGet]
    [Route("{shoppingCartId:guid}")]
    public async Task<ShoppingCartResponse> GetShoppingCart(GetShoppingCartQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddToCart(AddToCartCommand command)
    {
        await Mediator.Send(command);

        return CreatedAtAction("GetShoppingCart", new GetShoppingCartQuery(command.UserId, command.ShoppingCartId));
    }
}