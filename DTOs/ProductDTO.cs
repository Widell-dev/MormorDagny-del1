namespace MormorDagnys.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<GetSupplierProductDto> Suppliers { get; set; }

    }
}