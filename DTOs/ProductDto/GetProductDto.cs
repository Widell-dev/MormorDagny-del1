using MormorDagnys.DTOs;

namespace MormorDagnys;

public class GetProductDto: BaseProductDto
{
public string ItemNumber { get; set; }
public List<ProductSupplierDto> Suppliers { get; set; }
}
