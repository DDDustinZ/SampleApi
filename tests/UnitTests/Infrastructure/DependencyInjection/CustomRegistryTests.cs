using CompanyName.SampleApi.Infrastructure.DependencyInjection;
using Lamar;

namespace CompanyName.SampleApi.UnitTests.Infrastructure.DependencyInjection;

public class CustomRegistryTests
{
    private readonly Container _sut;

    public CustomRegistryTests()
    {
        _sut = new Container(x => x.IncludeRegistry<CustomRegistry>());
    }
}