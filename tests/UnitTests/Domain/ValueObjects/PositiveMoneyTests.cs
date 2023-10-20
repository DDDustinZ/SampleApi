using System;
using CompanyName.SampleApi.Domain.Exceptions;
using CompanyName.SampleApi.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

// ReSharper disable ObjectCreationAsStatement

namespace CompanyName.SampleApi.UnitTests.Domain.ValueObjects;

public class PositiveMoneyTests
{
    [Fact]
    public void Should_ThrowException_When_CreatingOrderMoneyWithLessThanZeroAmount()
    {
        //Act
        Action act = () => new PositiveMoney(-1);

        //Assert
        act.Should().Throw<PositiveMoneyCannotBeLessThanZeroException>();
    }

    [Theory]
    [InlineData(10, 10, false)]
    [InlineData(10, 9, true)]
    [InlineData(9, 10, false)]
    public void Should_ReturnExpected_When_CallingGreaterThanOperator(int firstPennies, int secondPennies, bool expected)
    {
        //Arrange
        var first = new PositiveMoney(firstPennies);
        var second = new PositiveMoney(secondPennies);

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
        var first = new PositiveMoney(firstPennies);
        var second = new PositiveMoney(secondPennies);

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
        var first = new PositiveMoney(firstPennies);
        var second = new PositiveMoney(secondPennies);

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
        var first = new PositiveMoney(firstPennies);
        var second = new PositiveMoney(secondPennies);

        //Act
        var result = first <= second;

        //Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, 10, 20)]
    [InlineData(12, 15, 27)]
    [InlineData(10, 0, 10)]
    public void Should_ReturnTotal_When_CallingPlusOperator(int firstPennies, int secondPennies, int total)
    {
        //Arrange
        var first = new PositiveMoney(firstPennies);
        var second = new PositiveMoney(secondPennies);

        //Act
        var result = first + second;

        //Assert
        result.Pennies.Should().Be(total);
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10, 0, 10)]
    [InlineData(10, 20, 0)]
    public void Should_ReturnTotal_When_CallingMinusOperator(int firstPennies, int secondPennies, int total)
    {
        //Arrange
        var first = new PositiveMoney(firstPennies);
        var second = new PositiveMoney(secondPennies);

        //Act
        var result = first - second;

        //Assert
        result.Pennies.Should().Be(total);
    }

    [Theory]
    [InlineData(10, 5, 50)]
    [InlineData(2, 2, 4)]
    [InlineData(5, 5, 25)]
    public void Should_MultiplyOrderMoney_When_MultiplyingByQuantity(int pennies, int multiple, int total)
    {
        //Arrange
        var money = new PositiveMoney(pennies);
        var quantity = new Quantity(multiple);

        //Act
        var result = money * quantity;

        //Assert
        result.Pennies.Should().Be(total);
    }

    [Fact]
    public void Should_ImplicitConvertToPositiveMoney_When_SettingToInt()
    {
        //Act
        var money = (PositiveMoney)1050;

        //Assert
        money.Pennies.Should().Be(1050);
    }

    [Fact]
    public void Should_ImplicitConvertToInt()
    {
        //Arrange
        var money = new Money(1050);

        //Act
        var response = (int)money;

        //Assert
        response.Should().Be(1050);
    }

    [Fact]
    public void Should_ZeroPennies_When_CreatingZero()
    {
        //Act
        var sut = PositiveMoney.Zero();

        //Assert
        sut.Pennies.Should().Be(0);
    }
}