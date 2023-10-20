using System;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface IAsyncUpdatableRepository<in TAggregateRoot, in TId> where TAggregateRoot : AggregateRoot<TId> where TId : IEquatable<TId>
{
    void Attach(TAggregateRoot entity);
}