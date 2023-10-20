using System;
using System.Data.Common;
using System.Threading.Tasks;
using CompanyName.SampleApi.Api;
using CompanyName.SampleApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Xunit;

namespace CompanyName.SampleApi.IntegrationTests.TestFixture;

public class DatabaseTestFixture : IAsyncLifetime
{
    private DbConnection _dbConnection;
    private Respawner _respawner;

    public IServiceProvider Services { get; private set; }
    public SalesDbContext SalesDbContext { get; set; }

    public async Task InitializeAsync()
    {
        var factory = new WebApplicationFactory<Program>();
        Services = factory.Services.CreateScope().ServiceProvider;

        SalesDbContext = Services.GetRequiredService<SalesDbContext>();
        await SalesDbContext.Database.EnsureCreatedAsync();
        
        //Ensure Context doesn't return objects from memory between inserts and reads
        SalesDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        
        _dbConnection = new SqlConnection(SalesDbContext.Database.GetConnectionString());
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            SchemasToInclude = new []{ "dbo" },
            WithReseed = true
        });
    }

    public async Task ResetDb()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public async Task DisposeAsync()
    {
        await _dbConnection.CloseAsync();
        await SalesDbContext.Database.EnsureDeletedAsync();
    }
}