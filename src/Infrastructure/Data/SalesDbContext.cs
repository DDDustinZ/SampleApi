using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.SampleApi.Infrastructure.Data;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLineItem> OrderLineItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartLineItem> ShoppingCartLineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Owned<Quantity>();
        modelBuilder.Owned<PositiveMoney>();
        modelBuilder.Owned<Money>();
        modelBuilder.Owned<ProductName>();
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Quantity>().HaveConversion<QuantityConverter>();
        configurationBuilder.Properties<PositiveMoney>().HaveConversion<PositiveMoneyConverter>();
        configurationBuilder.Properties<Money>().HaveConversion<MoneyConverter>();
        configurationBuilder.Properties<ProductName>().HaveConversion<ProductNameConverter>();
    }
}