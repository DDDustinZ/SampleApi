using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.Exceptions;
using CompanyName.SampleApi.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.SampleApi.Infrastructure.Data.Repositories;

public abstract class AsyncRepository<TAggregateRoot, TId> : IAsyncRepository<TAggregateRoot, TId>
    where TAggregateRoot : AggregateRoot<TId> where TId : IEquatable<TId>
{
    private readonly DbSet<TAggregateRoot> _dbSet;

    protected AsyncRepository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<TAggregateRoot>();
    }

    public async Task<TAggregateRoot> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken) ?? throw new RecordNotFoundException();
    }

    public async Task<TAggregateRoot> GetBySpecAsync(Specification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(specification.Expression).FirstOrDefaultAsync(cancellationToken) ?? throw new RecordNotFoundException();
    }

    public async Task<IEnumerable<TAggregateRoot>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TAggregateRoot>> GetAllBySpecAsync(Specification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(specification.Expression).ToListAsync(cancellationToken);
    }

    public async Task<int> GetCount(Specification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(specification.Expression).CountAsync(cancellationToken);
    }

    public void Insert(TAggregateRoot entity)
    {
        _dbSet.Add(entity);
    }

    public void Attach(TAggregateRoot entity)
    {
        _dbSet.Attach(entity);
    }

    public void Delete(TAggregateRoot entity)
    {
        _dbSet.Remove(entity);
    }

    private IQueryable<TAggregateRoot> Query()
    {
        return _dbSet.AsNoTracking();
    }
}