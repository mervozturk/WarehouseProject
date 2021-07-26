using Amazon.CodeDeploy.Model;
using Core.Hashing;
using Core.Results;
using DataAccess.AWSclouds.DynamoDB;
using DataAccess.AWSclouds.RDS;
using Entities;
using System;
using System.Collections.ObjectModel;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            //RDSProduct product = new RDSProduct();
            //ObservableCollection<Product> products = product.GetAll("SELECT * FROM Warehouse.Products").Data;
            //for (int i = 0; i < products.Count; i++)
            //{
            //    product.Update(new Product { 
            //        CategoryId = 3,
            //        Description = products[i].Description,
            //        Id = products[i].Id,
            //        ProductName = products[i].ProductName,
            //        UnitPrice = products[i].UnitPrice,
            //        UnitsInStock = products[i].UnitsInStock,
            //        WarehouseID = products[i].WarehouseID
            //    });
            //}

            //RDSCategory category = new RDSCategory();
            //category.Delete(new Category { CategoryId = 1 });

            //RDSWarehouse warehouse = new RDSWarehouse();
            //warehouse.Add(new Warehouse { CustomerID = 1, WarehouseName = "AkvadiDepo" });

            //RDSCustomer customer = new RDSCustomer();
            //customer.Add(new Customer { CompanyName = "Tdd aş" });


            DynamoDBProduct product = new DynamoDBProduct();
            Result result = product.Add(new Product { Id = 2, CategoryId = 1, WarehouseID = 1, ProductName = "Jel Kalem", UnitPrice = 100, UnitsInStock = 10, Description = "Pensan Myking" });
            Console.WriteLine(result.Success);

        }
    }
}
 