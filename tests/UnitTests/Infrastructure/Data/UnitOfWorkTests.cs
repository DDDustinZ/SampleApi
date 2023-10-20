using System.Collections.Generic;
using System.Threading;
using CompanyName.SampleApi.Domain.DomainEvent;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Infrastructure.Data;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CompanyName.SampleApi.UnitTests.Infrastructure.Data;

public class UnitOfWorkTests
{
    [Theory]
    [AutoMoqData]
    public void Should_PublishDomainEvents_When_SavingChanges(
        CancellationToken cancellationToken,
        List<BaseDomainEvent> domainEvents, 
        TestEntity testEntity, 
        Mock<IMediator> mediatorMoq)
    {
        // Arrange
        var context = new TestDbContext(new DbContextOptionsBuilder().Options);
        testEntity.AddDomainEvents(domainEvents);
        context.TestEntities.Add(testEntity);
        var sut = new TestUnitOfWork(context, mediatorMoq.Object);

        // Act
        sut.SaveChangesAsync(cancellationToken);

        // Assert
        foreach (BaseDomainEvent baseDomainEvent in domainEvents)
        {
            mediatorMoq.Verify(x => x.Publish(baseDomainEvent, cancellationToken));
        }
    }
        
    public class TestUnitOfWork : UnitOfWork
    {
        public TestUnitOfWork(DbContext context, IMediator mediator) : base(context, mediator)
        {
        }
    }
        
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) 
            : base(options)
        {
        }
            
        public DbSet<TestEntity> TestEntities { get; set; }
    }

    public class TestEntity : Entity<int>
    {
        public void AddDomainEvents(List<BaseDomainEvent> domainEvents)
        {
            domainEvents.ForEach(AddDomainEvent);
        }
    }
}