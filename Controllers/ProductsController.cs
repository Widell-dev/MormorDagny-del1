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
        .Select( p => new
        {
         p.Id,
         p.ProductName,
         
         Suppliers = p.SupplierProducts
            .Select(c => new GetSupplierProductDto
            {
                SupplierId = c.SupplierId,
                SupplierName = c.Supplier.SupplierName,
                Email = c.Supplier.Email,
                PricePerKg = c.PricePerKg
            })
        }).ToListAsync();
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await context.Products
        .Where(c => c.Id == id)
        .Select(p => new ProductDTO
        {
            ProductId = p.Id,
            ProductName = p.ProductName,
            Suppliers = p.SupplierProducts
            .Select(sp => new GetSupplierProductDto
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
            return Ok(new{Success = true, StatusCode = 200, items=1, Data = product});
        }
        return NotFound();
    }
}
