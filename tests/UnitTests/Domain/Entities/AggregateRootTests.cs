using System.Collections.Generic;
using CompanyName.SampleApi.Domain.DomainEvent;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using FluentAssertions;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Domain.Entities;

public class AggregateRootTests
{
    [Theory]
    [AutoMoqData]
    public void Should_AddDomainEvents_When_InvokingAddDomainEvent(List<BaseDomainEvent> domainEvents, TestAggregateRoot aggregateRoot)
    {
        // Act
        aggregateRoot.TestAddDomainEvent(domainEvents[0]);
        aggregateRoot.TestAddDomainEvent(domainEvents[1]);
        aggregateRoot.TestAddDomainEvent(domainEvents[2]);

        // Assert
        aggregateRoot.DomainEvents.Should().BeEquivalentTo(domainEvents);
    }

    [Theory]
    [AutoMoqData]
    public void Should_EmptyDomainEvents_When_InvokingClearEvents(List<BaseDomainEvent> domainEvents, TestAggregateRoot aggregateRoot)
    {
        // Arrange
        aggregateRoot.TestAddDomainEvent(domainEvents[0]);
        aggregateRoot.TestAddDomainEvent(domainEvents[1]);
        aggregateRoot.TestAddDomainEvent(domainEvents[2]);

        // Act
        aggregateRoot.ClearEvents();

        // Assert
        aggregateRoot.DomainEvents.Should().BeEmpty();
    }

    public class TestAggregateRoot : AggregateRoot<int>
    {
        public void TestAddDomainEvent(BaseDomainEvent domainEvent) => base.AddDomainEvent(domainEvent);
    }
}