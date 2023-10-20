using CompanyName.SampleApi.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyName.SampleApi.Infrastructure.Data.Converters;

public class MoneyConverter : ValueConverter<Money, int>
{
    public MoneyConverter()
        : base(
            v => v.Pennies,
            v => new Money(v))
    {
    }
}