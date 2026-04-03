using MormorDagnys.DTOs;

namespace MormorDagnys;

public class GetProductsDto: BaseProductDto
{
public string ItemNumber { get; set; }
public List<ProductSupplierDto> Suppliers { get; set; }
}
