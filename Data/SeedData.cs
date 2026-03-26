using Microsoft.EntityFrameworkCore;
using MormorDagnys.Controllers;
using MormorDagnys.Data;
using MormorDagnys.Entities;

namespace MormorDagnys;

public class SeedData
{
    public async Task InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MormorDagnysContext>();
        await SeedDataBase(context);
    }

    private async Task SeedDataBase(MormorDagnysContext context)
    {
        await context.Database.MigrateAsync();

        await SeedSuppliers(context);
        await SeedProducts(context);
        await SeedSupplierProducts(context);
    }
    private static async Task SeedSuppliers(MormorDagnysContext context)
    {
        if (context.Suppliers.Any())
        return;
        var suppliers = new List<Supplier>
        {
            new(){SupplierName = "Hasses Bakkomponenter", ContactPerson = "Hasse", Address = "Mandelmassagatan 12",  Email = "Hasse@Hassesbk.se", PhoneNumber = "078323489324"},
            new(){SupplierName = "Lasses Bakkomponenter", ContactPerson = "Lasse", Address = "Mandelmassagatan 13", Email = "Lasse@Lassesbk.se", PhoneNumber = "078323489325"},
            new(){SupplierName = "Frasses Bakkomponenter", ContactPerson = "Frasse", Address = "Mandelmassagatan 14", Email = "Frasse@Lassesbk.se", PhoneNumber = "078323489326"}
        };
        context.Suppliers.AddRange(suppliers);
        await context.SaveChangesAsync();
    }
    private static async Task SeedProducts(MormorDagnysContext context)
    {
        if(context.Products.Any())
        return;
        var products = new List<Product>
        {
            new() {ProductName = "Mjöl", PricePerKg = 22},
            new() {ProductName = "Socker", PricePerKg = 32},
            new() {ProductName = "Bakpulver", PricePerKg = 38},
            new() {ProductName = "Smör", PricePerKg = 89},
            new() {ProductName = "Vaniljsocker", PricePerKg = 54},
        };
        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
    private async Task SeedSupplierProducts(MormorDagnysContext context)
    {
        if (context.SupplierProducts.Any()) return;

        var suppliers = await context.Suppliers.OrderBy(s => s.Id ).ToListAsync();
        var products = await context.Products.OrderBy(p => p.Id).ToListAsync();

        var supplierProducts = new List<SupplierProduct>
        {
            new() { SupplierId = suppliers[0].Id, ProductId = products[0].Id, PricePerKg = products[0].PricePerKg },
            new() { SupplierId = suppliers[0].Id, ProductId = products[1].Id, PricePerKg = products[1].PricePerKg},
            new() { SupplierId = suppliers[1].Id, ProductId = products[1].Id, PricePerKg = products[1].PricePerKg },
            new() { SupplierId = suppliers[1].Id, ProductId = products[2].Id, PricePerKg = products[2].PricePerKg},
            new() { SupplierId = suppliers[2].Id, ProductId = products[4].Id, PricePerKg = products[4].PricePerKg},
            new() { SupplierId = suppliers[2].Id, ProductId = products[0].Id, PricePerKg = products[0].PricePerKg}
        };

        context.SupplierProducts.AddRange(supplierProducts);
        await context.SaveChangesAsync();
    }
}
