using Core.FileHelper;
using Core.Results;
using DataAccess.Abstact;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.AWSclouds.S3
{
    public class S3Product : IAWSclouds<Product>
    {
        public Result Add(Product entity)
        {
            string data = entity.Id + "," + entity.CategoryId + "," + entity.WarehouseID + "," + entity.ProductName + "," + entity.UnitPrice + "," + entity.UnitsInStock + "," + entity.Description;
            FileManager.Write(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", data);
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", "warehouses3", null, "product"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public Result Delete(Product entity)
        {
            ObservableCollection<Product> collection = GetAll().Data;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.Id == collection[i].Id)
                {
                    FileManager.Delete(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", i);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", "warehouses3", null, "product"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public DataResult<Product> Get(Func<Product, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", "warehouses3", null, "product");
            if (filter != null)
            {
                ObservableCollection<Product> collection = GetAll(filter).Data;
                return new SuccessDataResult<Product>(collection[0]);
            }
            return new ErrorDataResult<Product>();
        }

        public DataResult<ObservableCollection<Product>> GetAll(Func<Product, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", "warehouses3", null, "product");
            ObservableCollection<Product> Collection = new ObservableCollection<Product>();
            List<string> Lines = FileManager.Read(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt");
            string[] liste = new string[] { };
            string satir = "";
            for (int i = 0; i < Lines.Count; i++)
            {
                satir = Lines[i];
                if (satir.Trim() != "")
                {
                    liste = satir.Split(",");
                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(liste[0]),
                        CategoryId = Convert.ToInt32(liste[1]),
                        WarehouseID = Convert.ToInt32(liste[2]),
                        ProductName = liste[3],
                        UnitPrice = Convert.ToDouble(liste[4]),
                        UnitsInStock = Convert.ToInt32(liste[5]),
                        Description = liste[6]
                    };
                    Collection.Add(product);
                }
            }
            return filter == null ? new SuccessDataResult<ObservableCollection<Product>>(Collection) :
              new SuccessDataResult<ObservableCollection<Product>>((ObservableCollection<Product>)Collection.Where(filter));
        }

        public Result Update(Product entity)
        {
            ObservableCollection<Product> collection = GetAll().Data;
            string data = entity.Id + "," + entity.CategoryId + "," + entity.WarehouseID + "," + entity.ProductName + "," + entity.UnitPrice + "," + entity.UnitsInStock + "," + entity.Description;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.Id == collection[i].Id)
                {
                    FileManager.Update(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", i, data);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\product.txt", "warehouses3", null, "product"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
