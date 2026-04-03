namespace MormorDagnys.DTOs;

public class SupplierDetailsDto
{
public int Id { get; set; }
    public string SupplierName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public List<SupplierProductDto> Products { get; set; }

}
