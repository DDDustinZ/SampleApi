using CompanyName.SampleApi.Domain.Exceptions;

namespace CompanyName.SampleApi.Domain.ValueObjects;

public record Quantity
{
    public Quantity(int count)
    {
        if (count is < 1 or > 99)
            throw new QuantityOutOfRangeException();

        Count = count;
    }

    public int Count { get; }

    public static implicit operator int(Quantity quantity)
    {
        return quantity.Count;
    }

    public static implicit operator Quantity(int count)
    {
        return new Quantity(count);
    }
}