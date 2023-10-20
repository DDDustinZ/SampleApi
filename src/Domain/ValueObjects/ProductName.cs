using CompanyName.SampleApi.Domain.Exceptions;

namespace CompanyName.SampleApi.Domain.ValueObjects;

public record ProductName
{
    public ProductName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidProductNameException();

        Name = name;
    }

    public string Name { get; }

    public static implicit operator string(ProductName productName)
    {
        return productName.Name;
    }

    public static implicit operator ProductName(string name)
    {
        return new ProductName(name);
    }

    public override string ToString()
    {
        return Name;
    }
}