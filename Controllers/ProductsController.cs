using Microsoft.AspNetCore.Mvc;
using MormorDagnys.Data;
using Microsoft.EntityFrameworkCore;
using MormorDagnys.Entities;

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
         p.PricePerKg,
         Suppliers = p.SupplierProducts
            .Select(c => new
            {
                c.SupplierId,
                c.Supplier.SupplierName,
                c.Supplier.Email
            })
        }).ToListAsync();
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await context.Products
        .Where(c => c.Id == id)
        .Select(p => new
        {
            p.Id,
            p.PricePerKg,
            p.ProductName,

            Supplier= p.SupplierProducts
            .Select(sp => new
            {
                sp.SupplierId,
                sp.Supplier.SupplierName,
                sp.PricePerKg
            })
        })
        .SingleOrDefaultAsync();
        if(product is not null)
        {
            return Ok(new{Success = true, StatusCode = 200, items=1, Data = product});
        }
        return NotFound();
    }
}
