using CompanyName.SampleApi.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyName.SampleApi.Infrastructure.Data.Converters;

public class PositiveMoneyConverter : ValueConverter<PositiveMoney, int>
{
    public PositiveMoneyConverter()
        : base(
            v => v.Pennies,
            v => new PositiveMoney(v))
    {
    }
}