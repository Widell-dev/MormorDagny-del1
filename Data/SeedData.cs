using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MormorDagnys.Data;
using MormorDagnys.Entities;

namespace MormorDagnys.Data;

public class SeedData
{
    
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public async Task InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MormorDagnysContext>();

        await context.Database.MigrateAsync();

        await SeedSuppliers(context);
        await SeedProducts(context);
        await SeedSupplierProducts(context);
    }

    
    private static async Task SeedSuppliers(MormorDagnysContext context)
    {
        if(context.Suppliers.Any()) return;
        var json = await File.ReadAllTextAsync("seed-data/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        context.Suppliers.AddRange(suppliers);
        await context.SaveChangesAsync();
    }
    private static async Task SeedProducts(MormorDagnysContext context)
    {
        if(context.Products.Any()) return;
        var json = await File.ReadAllTextAsync("seed-data/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
        Console.WriteLine("PATH: " + Directory.GetCurrentDirectory());
        Console.WriteLine("FILE EXISTS: " + File.Exists("seed-data/products.json"));

    }
    private static async Task SeedSupplierProducts(MormorDagnysContext context)
    {
        if(context.SupplierProducts.Any()) return;

        var json = await File.ReadAllTextAsync("seed-data/supplierProducts.json");
        var supplierProducts = JsonSerializer.Deserialize<List<SupplierProduct>>(json, options);

        context.SupplierProducts.AddRange(supplierProducts);
        await context.SaveChangesAsync();

    }
    
}
