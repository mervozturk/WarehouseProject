using Core.Messages;
using Core.Results;
using DataAccess.Abstact;
using Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess.AWSclouds.RDS
{
    public class RDSProduct:IAWSclouds<Product>
    {
        public Result Add(Product entity)
        {
            string sqlQuery = "INSERT INTO Warehouse.Products(ID,WarehouseID,CategoryID,ProductName,UnitsInStock,UnitPrice,Description) VALUES('" + entity.Id + "','" + entity.WarehouseID + "','" + entity.CategoryId + "','" + entity.ProductName + "','" + entity.UnitsInStock + "','" + entity.UnitPrice + "','" + entity.Description + "')";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public Result Delete(Product entity)
        {
            string sqlQuery = "DELETE FROM Warehouse.Products WHERE ID='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public DataResult<Product> Get(Func<Product, bool> filter = null)
        {
            return filter == null ? new SuccessDataResult<Product>() :
                new SuccessDataResult<Product>(GetAll().Data.Single(filter));
        }

        public DataResult<ObservableCollection<Product>> GetAll(Func<Product, bool> filter = null)
        {
            string sqlQuery = "SELECT* FROM Warehouse.Products ";
            DataResult<DataTable> result = RDSBase.Get(sqlQuery);

            ObservableCollection<Product> Products = new ObservableCollection<Product>();
            foreach (DataRow dataRow in result.Data.Rows)
            {
                Product product = new Product();
                product.Id = (int)dataRow["ID"];
                product.WarehouseID = (int)dataRow["WarehouseID"];
                product.CategoryId = (int)dataRow["CategoryID"];
                product.ProductName = (string)dataRow["ProductName"];
                product.UnitsInStock = (int)dataRow["UnitsInStock"];
                product.UnitPrice = (double)dataRow["UnitPrice"];
                product.Description = (string)dataRow["Description"];
                Products.Add(product);
            }
            return filter == null ? new SuccessDataResult<ObservableCollection<Product>>(Products) :
               new SuccessDataResult<ObservableCollection<Product>>((ObservableCollection<Product>)Products.Where(filter));
        }

        public Result Update(Product entity)
        {
            string sqlQuery = "UPDATE Warehouse.Products SET ID='" + entity.Id + "',WarehouseID='" + entity.WarehouseID + "',CategoryID='" + entity.CategoryId + "',ProductName='" + entity.ProductName + "',UnitsInStock='" + entity.UnitsInStock + "',UnitPrice='" + entity.UnitPrice+ "',Description='" + entity.Description + "' WHERE ID='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
