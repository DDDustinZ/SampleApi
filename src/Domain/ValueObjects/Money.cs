namespace CompanyName.SampleApi.Domain.ValueObjects;

public record Money
{
    protected Money()
    {
    }

    public Money(int pennies)
    {
        Pennies = pennies;
    }

    public static Money Zero() => new(0);

    public int Pennies { get; }

    // ReSharper disable once PossibleLossOfFraction
    public decimal Amount => (decimal)Pennies / 100;

    public override string ToString() => Amount.ToString("C");

    public static bool operator >(Money x, Money y)
    {
        return x.Pennies > y.Pennies;
    }

    public static bool operator <(Money x, Money y)
    {
        return x.Pennies < y.Pennies;
    }

    public static bool operator >=(Money x, Money y)
    {
        return x.Pennies >= y.Pennies;
    }

    public static bool operator <=(Money x, Money y)
    {
        return x.Pennies <= y.Pennies;
    }

    public static Money operator +(Money x, Money y)
    {
        return new Money(x.Pennies + y.Pennies);
    }

    public static Money operator -(Money x, Money y)
    {
        return new Money(x.Pennies - y.Pennies);
    }

    public static implicit operator int(Money money)
    {
        return money.Pennies;
    }

    public static implicit operator Money(int pennies)
    {
        return new Money(pennies);
    }
}