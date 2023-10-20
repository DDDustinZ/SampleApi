using System;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface IAsyncRepository<TAggregateRoot, in TId> :
    IAsyncGettableRepository<TAggregateRoot, TId>,
    IAsyncInsertableRepository<TAggregateRoot, TId>,
    IAsyncUpdatableRepository<TAggregateRoot, TId>,
    IAsyncDeletableRepository<TAggregateRoot, TId>
    where TAggregateRoot : AggregateRoot<TId> where TId : IEquatable<TId>
{
}