using AutoFixture;
using AutoFixture.Kernel;
using Bogus;
using CompanyName.SampleApi.Domain.ValueObjects;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture.SpecimenBuilders;

public class PositiveMoneySpecimenBuilder : SpecimenBuilderBase<PositiveMoney>
{
    protected override PositiveMoney CreateObject(object request, ISpecimenContext context)
    {
        var faker = context.Create<Faker>();
        return new PositiveMoney(faker.Random.Int(0, 1000000));
    }
}