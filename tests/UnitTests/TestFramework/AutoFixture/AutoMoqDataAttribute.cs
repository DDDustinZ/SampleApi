using System;
using AutoFixture.Xunit2;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() : base(AutoFixtureFactory.GetDefaultFixture)
    { }

    public AutoMoqDataAttribute(params Type[] customizations)
        : base(() => AutoFixtureFactory.GetCustomizedFixture(customizations))
    { }
}