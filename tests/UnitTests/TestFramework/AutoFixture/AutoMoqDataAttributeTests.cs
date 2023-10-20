using System.Collections.Generic;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;

public class AutoMoqDataAttributeTests
{
    [Theory]
    [AutoMoqData]
    public void Should_GenerateAnonymousData_When_PassingParametersIntoTest(TestClass testClass)
    {
        //Assert
        testClass.Int.Should().NotBe(0);
        testClass.String.Should().NotBeEmpty();
        testClass.Strings.Should().HaveCount(3);
        testClass.Strings.Should().NotBeEmpty();
    }

    [Theory]
    [AutoMoqData]
    public void Should_ReturnSameFixtureInstance_When_PassingFixtureAsParameter([Frozen] int x, Fixture fixture)
    {
        //Assert
        fixture.Create<int>().Should().Be(x);
    }

    [Theory]
    [AutoMoqData(typeof(TestStringSpecimenBuilder), typeof(TestIntSpecimenBuilder))]
    public void Should_ApplyCustomizations_When_PassingCustomizations(string s, int i)
    {
        //Assert
        s.Should().Be("hello");
        i.Should().Be(5);
    }

    public class TestClass
    {
        public int Int { get; set; }
        public string String { get; set; }
        public List<string> Strings { get; set; }
    }

    private class TestStringSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;

            if (pi == null || pi.ParameterType != typeof(string))
                return new NoSpecimen();

            return "hello";
        }
    }

    private class TestIntSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;

            if (pi == null || pi.ParameterType != typeof(int))
                return new NoSpecimen();

            return 5;
        }
    }
}