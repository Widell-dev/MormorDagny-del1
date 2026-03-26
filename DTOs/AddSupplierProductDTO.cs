using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MormorDagnys.DTOs
{
    public class AddSupplierProductDTO
    {
        public int ProductId { get; set; }
        public decimal PricePerKg { get; set; }
    }
}