using Microsoft.AspNetCore.Mvc;
using MormorDagnys.Data;
using Microsoft.EntityFrameworkCore;
using MormorDagnys.Entities;
using MormorDagnys.DTOs;

namespace MormorDagnys.Controllers;

    [Route("api/products")]

    public class ProductsController(MormorDagnysContext context) : ControllerBase
    {
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await context.Products
        .Select( p => new GetProductsDto
        {
         Id = p.Id,
         ProductName = p.ProductName,
            Suppliers = p.SupplierProducts
            .Select(sp => new ProductSupplierDto
            {
                SupplierId = sp.SupplierId,
                SupplierName = sp.Supplier.SupplierName,
                Email = sp.Supplier.Email,
                PricePerKg = sp.PricePerKg

            }).ToList()
        }).ToListAsync();
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await context.Products
        .Where(c => c.Id == id)
        .Select(p => new GetProductDto
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Suppliers = p.SupplierProducts
            .Select(sp => new ProductSupplierDto
            {
                SupplierId = sp.SupplierId,
                SupplierName = sp.Supplier.SupplierName,
                Email = sp.Supplier.Email,
                PricePerKg = sp.PricePerKg
            })
            .ToList()
            
        })
        .SingleOrDefaultAsync();
        if(product is not null)
        {
            return NotFound("Product not found");
        }
        return Ok(product);
    }
}
