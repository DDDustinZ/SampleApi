using CompanyName.SampleApi.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyName.SampleApi.Infrastructure.Data.Converters;

public class ProductNameConverter : ValueConverter<ProductName, string>
{
    public ProductNameConverter()
        : base(
            v => v.Name,
            v => new ProductName(v))
    {
    }
}