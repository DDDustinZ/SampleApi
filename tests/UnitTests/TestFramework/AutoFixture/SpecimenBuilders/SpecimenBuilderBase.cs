using System;
using AutoFixture.Kernel;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture.SpecimenBuilders;

public abstract class SpecimenBuilderBase<T> : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request as Type != typeof(T))
        {
            return new NoSpecimen();
        }

        return CreateObject(request, context);
    }

    protected abstract T CreateObject(object request, ISpecimenContext context);
}