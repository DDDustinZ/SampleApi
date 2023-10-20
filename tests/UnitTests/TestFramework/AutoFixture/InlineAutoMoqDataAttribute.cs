using System;
using AutoFixture.Xunit2;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;

public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects) { }

    public InlineAutoMoqDataAttribute(Type[] customizations, params object[] objects) : base(new AutoMoqDataAttribute(customizations), objects) { }
}