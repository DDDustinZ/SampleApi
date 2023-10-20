using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.Specifications;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface IAsyncGettableRepository<TAggregateRoot, in TId> where TAggregateRoot : AggregateRoot<TId> where TId : IEquatable<TId>
{
    Task<TAggregateRoot> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<TAggregateRoot> GetBySpecAsync(Specification<TAggregateRoot> specification, CancellationToken cancellationToken = default);
    Task<IEnumerable<TAggregateRoot>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TAggregateRoot>> GetAllBySpecAsync(Specification<TAggregateRoot> specification, CancellationToken cancellationToken = default);
    Task<int> GetCount(Specification<TAggregateRoot> specification, CancellationToken cancellationToken = default);
}