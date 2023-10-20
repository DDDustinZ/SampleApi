using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.SampleApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.SampleApi.Infrastructure.Data;

public abstract class UnitOfWork
{
    private readonly DbContext _context;
    private readonly IMediator _mediator;

    protected UnitOfWork(DbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await SaveChangesAsync(new CancellationToken());
    }
        
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var numberOfAffectedRows = await _context.SaveChangesAsync(cancellationToken);
            
        var changedAggregates = _context.ChangeTracker.Entries<IEntityWithDomainEvents>()
            .Select(x => x.Entity)
            .ToArray();
            
        await PublishDomainEvents(changedAggregates, cancellationToken);

        return numberOfAffectedRows;
    }

    private async Task PublishDomainEvents(IEnumerable<IEntityWithDomainEvents> changedEntities, CancellationToken cancellationToken)
    {
        foreach (var changedEntity in changedEntities)
        {
            foreach (var domainEvent in changedEntity.DomainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            changedEntity.ClearEvents();
        }
    }
}