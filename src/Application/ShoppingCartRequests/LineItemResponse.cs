namespace CompanyName.SampleApi.Application.ShoppingCartRequests;

public record LineItemResponse(string Name, int Price, int Quantity)
{
}