using KellermanSoftware.CompareNetObjects;

namespace CompanyName.SampleApi.UnitTests.TestFramework;

public static class CompareExtensions
{
    public static bool VerifyDeepEqual(this object first, object second)
    {
        first.ShouldCompare(second);
        return true;
    }

    public static bool VerifyDeepEqual(this object first, object second, ComparisonConfig config)
    {
        first.ShouldCompare(second, compareConfig: config);
        return true;
    }
}