using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MormorDagnys.DTOs
{
    public class GetSupplierProductDto
    {
    
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public decimal PricePerKg { get; set; }
    }
}
