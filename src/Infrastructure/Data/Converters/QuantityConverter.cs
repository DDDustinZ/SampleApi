using CompanyName.SampleApi.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyName.SampleApi.Infrastructure.Data.Converters;

public class QuantityConverter : ValueConverter<Quantity, int>
{
    public QuantityConverter()
        : base(
            v => v.Count,
            v => new Quantity(v))
    {
    }
}