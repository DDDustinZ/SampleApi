using System.Threading.Tasks;
using CompanyName.SampleApi.Application.Interfaces.Data;
using CompanyName.SampleApi.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompanyName.SampleApi.IntegrationTests.TestFixture;

[Collection(nameof(DatabaseTestFixture))]
public abstract class RepositoryTestBase : IClassFixture<DatabaseTestFixture>, IAsyncLifetime
{
    private readonly DatabaseTestFixture _fixture;

    protected RepositoryTestBase(DatabaseTestFixture fixture)
    {
        _fixture = fixture;
        SalesDbContext = fixture.SalesDbContext;
        ScopedUnitOfWork = fixture.Services.GetRequiredService<ISalesUnitOfWork>();
    }

    protected SalesDbContext SalesDbContext { get; set; }
    public ISalesUnitOfWork ScopedUnitOfWork { get; set; }

    public async Task InitializeAsync()
    {
        await _fixture.ResetDb();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}