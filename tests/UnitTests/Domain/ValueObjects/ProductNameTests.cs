using System;
using CompanyName.SampleApi.Domain.Exceptions;
using CompanyName.SampleApi.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

// ReSharper disable ObjectCreationAsStatement

namespace CompanyName.SampleApi.UnitTests.Domain.ValueObjects;

public class ProductNameTests
{
    [Fact]
    public void Should_SetName_When_CallingConstructor()
    {
        //Act
        var productName = new ProductName("Some Name");

        //Assert
        productName.Name.Should().Be("Some Name");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_ThrowException_When_CallingConstructorWithInvalidName(string name)
    {
        //Act
        Action act = () => new ProductName(name);

        //Assert
        act.Should().Throw<InvalidProductNameException>();
    }

    [Fact]
    public void Should_ReturnNameString_When_ToString()
    {
        //Act
        var productName = new ProductName("Some Name");

        //Assert
        productName.ToString().Should().Be("Some Name");
    }

    [Fact]
    public void Should_ImplicitConvertToProductName_When_SettingToString()
    {
        //Act
        var productName = (ProductName) "Some Name";

        //Assert
        productName.Name.Should().Be("Some Name");
    }

    [Fact]
    public void Should_ImplicitConvertToString()
    {
        //Arrange
        var productName = new ProductName("Some Name");

        //Act
        var response = (string)productName;

        //Assert
        response.Should().Be("Some Name");
    }
}