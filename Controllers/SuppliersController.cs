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
        .Where(s => s.Id == supplierId)
        .Select(s => new SupplierDetailsDto
        {
            Id = s.Id,
            SupplierName = s.SupplierName,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber,
            Email = s.Email,
            Products = s.SupplierProducts.Select(sp => new SupplierProductDto
            {
                ProductId = sp.ProductId,
                ProductName = sp.Product.ProductName,
                PricePerKg = sp.PricePerKg
            }).ToList()
        }).FirstOrDefaultAsync();
        if (supplier is null) return NotFound("Supplier not found");


        return Ok(supplier);
    }
    [HttpPost("{supplierId}/products")]
    public async Task <ActionResult> AddProductToSupplier(int supplierId, PostSupplierProductDTO dto)
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

        return Ok("Product added to supplier");
    }
    [HttpPatch("{supplierId}/products/{productId}")]
    public async Task <ActionResult> UpdateSupplierProductPrice(int supplierId, int productId, PutSupplierProductDto dto)
    {
        var supplierproduct = await context.SupplierProducts
            .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);

        if(supplierproduct is null)return NotFound("Produkten finns inte hos leverantören");

        supplierproduct.PricePerKg = dto.PricePerKg;

        await context.SaveChangesAsync();
        return Ok("Priset har uppdaterats");
    }
    }
