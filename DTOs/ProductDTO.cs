namespace MormorDagnys.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<SupplierProductDTO> Suppliers { get; set; }

    }
}