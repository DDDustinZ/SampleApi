using System;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;

public class AutoFixtureExtensionsTests
{
    [Theory]
    [AutoMoqData]
    public void Should_ReturnResult_When_CallingInvoke(Calculator calculator, Fixture fixture)
    {
        //Arrange
        fixture.Register(() => 5);

        //Act
        var sum = fixture.Invoke(calculator, nameof(calculator.Add));

        //Assert
        sum.Should().Be(10);
    }

    [Theory]
    [AutoMoqData]
    public void Should_HandleVoidMethods_When_CallingInvoke(Calculator calculator, Fixture fixture)
    {
        //Arrange
        fixture.Register(() => 5);

        //Act
        fixture.Invoke(calculator, nameof(calculator.SetSeed));

        //Assert
        calculator.Seed.Should().Be(5);
    }

    [Theory]
    [AutoMoqData]
    public void Should_ThrowException_When_CallingInvokeWithMethodThatDoesNotExist(Calculator calculator, Fixture fixture)
    {
        //Act
        Action act = () => fixture.Invoke(calculator, "notAMethod");

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    public class Calculator
    {
        public int Seed { get; set; }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public void SetSeed(int x)
        {
            Seed = x;
        }
    }
}