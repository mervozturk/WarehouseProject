using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ProductDTO
    {
        public string WareHouse { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public int UnitsInStock { get; set; }
        public double UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
