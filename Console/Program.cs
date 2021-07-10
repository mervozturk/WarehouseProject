using Core.Hashing;
using Core.Results;
using DataAccess.AWSclouds.RDS;
using Entities;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            RDSProduct product = new RDSProduct();
            product.Add(new Product { CategoryId = 1, WarehouseID = 1, ProductName = "kalem", UnitsInStock = 150, UnitPrice = 5, Description = " Siyah Jel kalem" });

            RDSCategory category = new RDSCategory();
            category.Add(new Category { CategoryName = "Kırtavsiye" });
            category.Add(new Category { CategoryName = "Elektronik" });
            category.Add(new Category { CategoryName = "Mutfak malzemeleri" });

            RDSWarehouse warehouse = new RDSWarehouse();
            warehouse.Add(new Warehouse { CustomerID = 1, WarehouseName = "AkvadiDepo" });

            RDSCustomer customer = new RDSCustomer();
            customer.Add(new Customer { CompanyName = "Tdd aş" });

        }
    }
}
