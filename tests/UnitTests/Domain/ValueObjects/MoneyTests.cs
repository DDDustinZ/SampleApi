using CompanyName.SampleApi.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Should_SetPennies_When_CallingConstructor()
    {
        //Act
        var money = new Money(112);

        //Assert
        money.Pennies.Should().Be(112);
        money.Amount.Should().Be(1.12m);
        money.ToString().Should().Be("$1.12");
    }

    [Theory]
    [InlineData(10, 10, false)]
    [InlineData(10, 9, true)]
    [InlineData(9, 10, false)]
    public void Should_ReturnExpected_When_CallingGreaterThanOperator(int firstPennies, int secondPennies, bool expected)
    {
        //Arrange
        var first = new Money(firstPennies);
        var second = new Money(secondPennies);

        //Act
        var result = first > second;

        //Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, 10, false)]
    [InlineData(10, 9, false)]
    [InlineData(9, 10, true)]
    public void Should_ReturnExpected_When_CallingLessThanOperator(int firstPennies, int secondPennies, bool expected)
    {
        //Arrange
        var first = new Money(firstPennies);
        var second = new Money(secondPennies);

        //Act
        var result = first < second;

        //Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, 10, true)]
    [InlineData(10, 9, true)]
    [InlineData(9, 10, false)]
    public void Should_ReturnExpected_When_CallingGreaterThanOrEqualOperator(int firstPennies, int secondPennies, bool expected)
    {
        //Arrange
        var first = new Money(firstPennies);
        var second = new Money(secondPennies);

        //Act
        var result = first >= second;

        //Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, 10, true)]
    [InlineData(10, 9, false)]
    [InlineData(9, 10, true)]
    public void Should_ReturnExpected_When_CallingLessThanOrEqualOperator(int firstPennies, int secondPennies, bool expected)
    {
        //Arrange
        var first = new Money(firstPennies);
        var second = new Money(secondPennies);

        //Act
        var result = first <= second;

        //Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, 10, 20)]
    [InlineData(10, 0, 10)]
    [InlineData(10, -5, 5)]
    [InlineData(-5, -5, -10)]
    public void Should_ReturnTotal_When_CallingPlusOperator(int firstPennies, int secondPennies, int total)
    {
        //Arrange
        var first = new Money(firstPennies);
        var second = new Money(secondPennies);

        //Act
        var result = first + second;

        //Assert
        result.Pennies.Should().Be(total);
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10, 0, 10)]
    [InlineData(10, -5, 15)]
    [InlineData(-5, -5, 0)]
    public void Should_ReturnTotal_When_CallingMinusOperator(int firstPennies, int secondPennies, int total)
    {
        //Arrange
        var first = new Money(firstPennies);
        var second = new Money(secondPennies);

        //Act
        var result = first - second;

        //Assert
        result.Pennies.Should().Be(total);
    }

    [Fact]
    public void Should_ImplicitConvertToMoney_When_SettingToInt()
    {
        //Act
        var money = (Money) 1050;

        //Assert
        money.Pennies.Should().Be(1050);
    }

    [Fact]
    public void Should_ImplicitConvertToInt()
    {
        //Arrange
        var money = new Money(1050);

        //Act
        var response = (int) money;

        //Assert
        response.Should().Be(1050);
    }

    [Fact]
    public void Should_ZeroPennies_When_CreatingZero()
    {
        //Act
        var sut = Money.Zero();

        //Assert
        sut.Pennies.Should().Be(0);
    }
}