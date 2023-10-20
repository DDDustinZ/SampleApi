using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace CompanyName.SampleApi.IntegrationTests.Configuration;

public class ConfigurationTests
{
    private readonly IConfigurationRoot _config;

    public ConfigurationTests()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }

    [Fact]
    public void Should_GetConnectionString_When_ReadingAppSettings()
    {
        //Act
        var connectionString = _config.GetConnectionString("SalesDbContext");

        //Assert
        connectionString.Should().Be("Server=(localdb)\\mssqllocaldb;Database=Sales;Trusted_Connection=True;");
    }

    [Fact]
    public void Should_GetCustomSettings_When_UsingGenericGet()
    {
        //Act
        var customSettings = _config.GetSection("CustomSettings").Get<CustomSettings>();

        //Assert
        customSettings.HasStuff.Should().BeTrue();
        customSettings.Stuff.Should().Be("Here is some stuff");
        customSettings.NumberOfStuff.Should().Be(10);
    }

    private class CustomSettings
    {
        public bool HasStuff { get; set; }
        public string Stuff { get; set; }
        public int NumberOfStuff { get; set; }
    }
}