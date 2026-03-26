namespace MormorDagnys.Entities;

public record Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal PricePerKg { get; set; }
    public List<SupplierProduct> SupplierProducts { get; set; }
}
