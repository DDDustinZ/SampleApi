using System;
using CompanyName.SampleApi.Domain.Entities;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface IAsyncInsertableRepository<in TAggregateRoot, in TId> where TAggregateRoot : AggregateRoot<TId> where TId : IEquatable<TId>
{
    void Insert(TAggregateRoot entity);
}