using System.Collections.Generic;

namespace CompanyName.SampleApi.Application.ShoppingCartRequests;

public record ShoppingCartResponse(string FullName, IEnumerable<LineItemResponse> Products);