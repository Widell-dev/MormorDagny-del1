using Microsoft.AspNetCore.Mvc;
using MormorDagnys.Entities;
using Microsoft.EntityFrameworkCore;
using MormorDagnys.Data;
using MormorDagnys.DTOs;

namespace MormorDagnys.Controllers;

    [Route("api/suppliers")]
    public class SuppliersController(MormorDagnysContext context) : ControllerBase
    {
        [HttpGet("products/{supplierId}")]
        public async Task<ActionResult> ListSupplierWithTheirProducts(int supplierId)
    {
        var supplier = await context.Suppliers
        .Include(s => s.SupplierProducts)
            .ThenInclude(sp => sp.Product)
        .FirstOrDefaultAsync(s => s.Id == supplierId);

        if(supplier is null) return NotFound($"Hitta ingen leverantör");

        var result = new{
            supplier.Id,
            supplier.SupplierName,
            supplier.Address,
            supplier.PhoneNumber,
            supplier.Email,
            Products = supplier.SupplierProducts
            .Select(p => new
            {
                p.ProductId,
                ProductsName = p.Product.ProductName,
                SupplierPrice = p.PricePerKg
            })
        };

        return Ok(result);
    }
    [HttpPost("{supplierId}/products")]
    public async Task <ActionResult> AddProductToSupplier(int supplierId, SupplierProductDTO dto)
    {
        Supplier supplier = await context.Suppliers.FindAsync(supplierId);
        if(supplier is null) return NotFound("Doesnt exist");

        Product product = await context.Products.FindAsync(dto.ProductId);
        if(product is null) return NotFound("Doesnt exist");

        SupplierProduct existing = await context.SupplierProducts
            .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == dto.ProductId);
        
        if(existing is not null)
        return BadRequest("It seems like you are trying to add duplicates");

        SupplierProduct sp = new()
        {
            Supplier = supplier,
            Product = product,
            PricePerKg = dto.PricePerKg
        };

        context.SupplierProducts.Add(sp);
        await context.SaveChangesAsync();

        return Ok();
    }
    [HttpPatch("{supplierId}/products/{productId}")]
    public async Task <ActionResult> UpdateSupplierProductPrice(int supplierId, int productId, UpdateSupplierProductPriceDto dto)
    {
        var supplierproduct = await context.SupplierProducts
            .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);

        if(supplierproduct is null)return NotFound("Produkten finns inte hos leverantören");

        supplierproduct.PricePerKg = dto.PricePerKg;

        await context.SaveChangesAsync();
        return Ok("Priset har uppdaterats");
    }
    }
