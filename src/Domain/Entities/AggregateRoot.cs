using System;

namespace CompanyName.SampleApi.Domain.Entities;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : IEquatable<TId>
{
}