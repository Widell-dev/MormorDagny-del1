using MormorDagnys.Entities;

namespace MormorDagnys.Entities;

public class SupplierProduct
{
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public decimal PricePerKg { get; set; }
}
