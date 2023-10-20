using System;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;

public static class AutoFixtureExtensions
{
    public static object Invoke<T>(this Fixture fixture, T obj, string methodName)
    {
        var methodInfo = typeof(T).GetMethod(methodName);

        if (methodInfo == null)
        {
            throw new ArgumentException("Method does not exist.");
        }

        var parameters = methodInfo.GetParameters().Select(x => fixture.Create(x.ParameterType, new SpecimenContext(fixture))).ToArray();
        return methodInfo.Invoke(obj, parameters);
    }
}