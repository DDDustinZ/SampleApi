using Xunit;

namespace CompanyName.SampleApi.IntegrationTests.TestFixture;

[CollectionDefinition(nameof(DatabaseTestFixture))]
public class DatabaseTestFixtureCollection : ICollectionFixture<DatabaseTestFixture>
{
}