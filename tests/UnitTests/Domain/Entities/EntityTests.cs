using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.SampleApi.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class EntityTests
{
    [Fact]
    public void Should_ReturnTrue_When_CallingEqualsOnTwoDifferentEntitiesWithSameIdButDifferentReferences()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity
        {
            Name = "Test2"
        };
        entity2.SetId(1);

        //Act
        var result = entity1.Equals(entity2);

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnFalse_When_CallingEqualsOnTwoDifferentEntitiesWithDifferentIds()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity
        {
            Name = "Test"
        };
        entity2.SetId(2);

        //Act
        var result = entity1.Equals(entity2);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_CallingEqualsWithNullEntity()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        //Act
        var result = entity1.Equals(null);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnTrue_When_CallingEqualsWithSameObjectReference()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        //Act
        var result = entity1.Equals(entity1);

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnFalse_When_CallingEqualsOnTwoDifferentEntitiesWithTwoDifferentTypedEntities()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity2
        {
            Name = "Test"
        };
        entity2.SetId(Guid.NewGuid());

        //Act
        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = entity1.Equals(entity2);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnTrue_When_CallingIsTransientOnEntityWithoutSettingId()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };

        var entity2 = new TestEntity2
        {
            Name = "Test"
        };

        //Act
        var result1 = entity1.IsTransient();
        var result2 = entity2.IsTransient();

        //Assert
        result1.Should().BeTrue();
        result2.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnFalse_When_CallingGetHashCodeOnTwoDifferentTypesOfEntitiesWithSameId()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity3
        {
            Name = "Test"
        };
        entity2.SetId(1);

        //Act
        var result = entity1.GetHashCode() == entity2.GetHashCode();

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnTrue_When_UsingTheEqualsOperatorWithTwoEntitiesWithSameIdentity()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity
        {
            Name = "Test"
        };
        entity2.SetId(1);

        //Act
        var result = entity1 == entity2;

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnFalse_When_UsingTheEqualsOperatorWithTwoEntitiesWithDifferentIdentities()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity
        {
            Name = "Test"
        };
        entity2.SetId(2);

        //Act
        var result = entity1 == entity2;

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    public void Should_ReturnTrue_When_UsingEqualsOperatorWithNullEntities()
    {
        //Arrange
        TestEntity entity1 = null;

        //Act
        var result = entity1 == null;

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnFalse_When_UsingTheNotEqualsOperatorWithTwoEntitiesWithSameIdentity()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity
        {
            Name = "Test"
        };
        entity2.SetId(1);

        //Act
        var result = entity1 != entity2;

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnTrue_When_UsingTheNotEqualsOperatorWithTwoEntitiesWithDifferentIdentities()
    {
        //Arrange
        var entity1 = new TestEntity
        {
            Name = "Test"
        };
        entity1.SetId(1);

        var entity2 = new TestEntity
        {
            Name = "Test"
        };
        entity2.SetId(2);

        //Act
        var result = entity1 != entity2;

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    public void Should_ReturnFalse_When_UsingNotEqualsOperatorWithNullEntities()
    {
        //Arrange
        TestEntity entity1 = null;

        //Act
        var result = entity1 != null;

        //Assert
        result.Should().BeFalse();
    }
}

class TestEntity : Entity<int>
{
    public string Name { get; set; }
    public void SetId(int id) => Id = id;
}

class TestEntity2 : Entity<Guid>
{
    public string Name { get; set; }
    public void SetId(Guid id) => Id = id;
}

class TestEntity3 : Entity<int>
{
    public string Name { get; set; }
    public void SetId(int id) => Id = id;
}