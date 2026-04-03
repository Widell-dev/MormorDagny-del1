using MormorDagnys.DTOs;

namespace MormorDagnys;

public class GetSupplierDto:BaseSupplierDto
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
