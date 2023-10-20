using CompanyName.SampleApi.Domain.Exceptions;

namespace CompanyName.SampleApi.Domain.ValueObjects;

public record PositiveMoney : Money
{
    public PositiveMoney(int pennies) : base(pennies)
    {
        if (pennies < 0)
            throw new PositiveMoneyCannotBeLessThanZeroException();
    }

    public new static PositiveMoney Zero() => new(0);

    public static bool operator >(PositiveMoney x, PositiveMoney y) => (Money) x > y;

    public static bool operator <(PositiveMoney x, PositiveMoney y) => (Money) x < y;

    public static bool operator >=(PositiveMoney x, PositiveMoney y) => (Money) x >= y;

    public static bool operator <=(PositiveMoney x, PositiveMoney y) => (Money) x <= y;

    public static PositiveMoney operator +(PositiveMoney x, PositiveMoney y)
    {
        var sum = (Money) x + y;
        return new PositiveMoney(sum.Pennies);
    }

    public static PositiveMoney operator -(PositiveMoney x, PositiveMoney y)
    {
        var difference = x.Pennies - y.Pennies;
        return difference < 0 ? new PositiveMoney(0) : new PositiveMoney(difference);
    }

    public static PositiveMoney operator *(PositiveMoney x, Quantity y)
    {
        return new PositiveMoney(x.Pennies * y.Count);
    }

    public static implicit operator int(PositiveMoney money)
    {
        return money.Pennies;
    }

    public static implicit operator PositiveMoney(int pennies)
    {
        return new PositiveMoney(pennies);
    }
}