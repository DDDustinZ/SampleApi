using System;
using CompanyName.SampleApi.Domain.Exceptions;
using CompanyName.SampleApi.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

// ReSharper disable ObjectCreationAsStatement

namespace CompanyName.SampleApi.UnitTests.Domain.ValueObjects;

public class QuantityTests
{
    [Fact]
    public void Should_SetCount_When_CallingConstructor()
    {
        //Act
        var quantity = new Quantity(10);

        //Assert
        quantity.Count.Should().Be(10);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(100)]
    public void Should_ThrowException_When_CallingConstructorWithQuantityOutOfRange(int value)
    {
        //Act
        Action act = () => new Quantity(value);

        //Assert
        act.Should().Throw<QuantityOutOfRangeException>();
    }

    [Fact]
    public void Should_ImplicitConvertToQuantity_When_SettingToInt()
    {
        //Act
        var quantity = (Quantity) 9;

        //Assert
        quantity.Count.Should().Be(9);
    }

    [Fact]
    public void Should_ImplicitConvertToInt()
    {
        //Arrange
        var quantity = new Quantity(9);

        //Act
        var response = (int) quantity;

        //Assert
        response.Should().Be(9);
    }
}