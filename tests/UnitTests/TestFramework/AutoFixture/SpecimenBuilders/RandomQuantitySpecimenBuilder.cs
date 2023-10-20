using AutoFixture;
using AutoFixture.Kernel;
using Bogus;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture.SpecimenBuilders;

public class RandomQuantitySpecimenBuilder : SpecimenBuilderBase<Quantity>
{
    protected override Quantity CreateObject(object request, ISpecimenContext context)
    {
        var faker = context.Create<Faker>();
        return new Quantity(faker.Random.Int(1, 100));
    }
}