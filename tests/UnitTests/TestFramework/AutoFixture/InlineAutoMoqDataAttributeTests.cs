using System.Reflection;
using AutoFixture.Kernel;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;

public class InlineAutoMoqDataAttributeTests
{
    [Theory]
    [InlineAutoMoqData("hello")]
    public void Should_BeAbleToPassInStaticData_When_UsingAutoMoqAttribute(string passedIn, string generated)
    {
        //Assert
        passedIn.Should().Be("hello");
        generated.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineAutoMoqData(new[]{ typeof(TestStringSpecimenBuilder), typeof(TestIntSpecimenBuilder) }, "not", 10)]
    public void Should_ApplyCustomizations_When_PassingCustomizations(string passedInString, int passedInInt, string generatedString, int generatedInt)
    {
        //Assert
        passedInString.Should().Be("not");
        passedInInt.Should().Be(10);
        generatedString.Should().Be("hello");
        generatedInt.Should().Be(5);
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