using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int WarehouseID { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public int UnitsInStock { get; set; }
        public double UnitPrice { get; set; }
        public string Description { get; set; }

    }
}
