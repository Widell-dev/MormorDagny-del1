﻿using MormorDagnys.Entities;
using Microsoft.EntityFrameworkCore;


namespace MormorDagnys.Data;

public class MormorDagnysContext(DbContextOptions<MormorDagnysContext> options)
    : DbContext(options)

{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet <Product> Products { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Sammansatt primary key för many-to-many mellan Supplier och Product
    modelBuilder.Entity<SupplierProduct>()
        .HasKey(sp => new { sp.SupplierId, sp.ProductId });
}

}
