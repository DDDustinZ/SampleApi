using System;
using System.Linq.Expressions;

namespace CompanyName.SampleApi.Domain.Specifications;

public abstract class Specification<T>
{
    public abstract bool IsSatisfied(T obj);

    public Expression<Func<T, bool>> Expression => obj => IsSatisfied(obj);
}