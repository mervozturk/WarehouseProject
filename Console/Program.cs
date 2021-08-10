using Core.FileHelper;
using Core.Hashing;
using Core.Results;
using DataAccess.AWSclouds.DynamoDB;
using DataAccess.AWSclouds.RDS;
using DataAccess.AWSclouds.S3;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

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


            //DynamoDBProduct product = new DynamoDBProduct();
            //Result result = product.Add(new Product { Id = 2, CategoryId = 1, WarehouseID = 1, ProductName = "Jel Kalem", UnitPrice = 100, UnitsInStock = 10, Description = "Pensan Myking" });
            //Console.WriteLine(result.Success);

            //DynamoDBUser user = new DynamoDBUser();
            //Result result1 = user.Add(new User { Id = 2, CustomerId = 2, Email = "Test@mail.com", FirstName = "Test", LastName = "Deneme", Password = "123456" });
            //Console.WriteLine(result1.Success);




            //List<string> data = FileManager.Read(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt");
            //for (int i = 0; i < data.Count; i++)
            //{
            //    Console.WriteLine(data[i]);
            //}
            //FileManager.Update(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", 1, "test");

            //FileManager.Update(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt",6, "son,satır");
            //Console.WriteLine("silindi yapıldı");
            //List<string> data2 = FileManager.Read(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt");
            //for (int i = 0; i < data2.Count; i++)
            //{
            //    Console.WriteLine(data2[i]);
            //}

            //File.Delete(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt");
            //S3Base s3Base = new S3Base();
            //s3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", "warehouses3", null, "product");

            S3User s3User = new S3User();
            //s3User.Update(new User { Id = 5, CustomerId = 2, FirstName = "Nagisa", LastName = "çelik", Email = "Nagisa@deneme.com", Password = "123456" });
            //s3User.Add(new User { Id = 6, CustomerId = 2, FirstName = "Ahmet", LastName = "Gürsoy", Email = "Ahmet@deneme.com", Password = "123456" });
            //DataResult<User> result = s3User.Get(p => p.Id == 1);
            //Console.WriteLine(result.Data.FirstName);
            ObservableCollection<User> users = s3User.GetAll().Data;
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine(users[i].Email);
            }
        }
    }
}
 