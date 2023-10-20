using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using Bogus;
using CompanyName.SampleApi.Domain.Entities;
using CompanyName.SampleApi.Domain.ValueObjects;
using CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture.SpecimenBuilders;

namespace CompanyName.SampleApi.UnitTests.TestFramework.AutoFixture;

public class AutoFixtureFactory
{
    public static Fixture GetDefaultFixture()
    {
        var autoFixture = new Fixture();
        autoFixture.Customize(new AutoMoqCustomization {ConfigureMembers = true});
        autoFixture.Register(() => new Faker());
            
        autoFixture.Customizations.Add(new RandomQuantitySpecimenBuilder());
        autoFixture.Customizations.Add(new PositiveMoneySpecimenBuilder());

        autoFixture.Register((Guid userId, IEnumerable<Product> products) =>
        {
            var shoppingCart = ShoppingCart.StartShopping(userId);
            foreach (var x in products)
            {
                shoppingCart.AddProduct(x);
            }
            return shoppingCart;
        });

        autoFixture.Register((ProductName name, PositiveMoney price, PositiveMoney tax, PositiveMoney shipping) => Product.AddToProductCatalog(name, price, tax, shipping));
        autoFixture.Register((Guid shoppingCardId, Product product) => ShoppingCartLineItem.AddNewLineItem(shoppingCardId, product));
        autoFixture.Register((Guid orderId, ShoppingCartLineItem shoppingCartLineItem) => OrderLineItem.ProcessLineItem(orderId, shoppingCartLineItem));
        autoFixture.Register((ShoppingCart shoppingCart) => Order.PlaceOrder(shoppingCart));

        return autoFixture;
    }

    public static Fixture GetCustomizedFixture(params Type[] customizations)
    {
        var fixture = GetDefaultFixture();

        foreach (var type in customizations)
        {
            var customization = Activator.CreateInstance(type) as ISpecimenBuilder;
            fixture.Customizations.Insert(0, customization);
        }

        return fixture;
    }
}