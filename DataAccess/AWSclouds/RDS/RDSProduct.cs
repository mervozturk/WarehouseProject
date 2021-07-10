using Core.Messages;
using Core.Results;
using DataAccess.Abstact;
using Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace DataAccess.AWSclouds.RDS
{
    public class RDSProduct : IAWSclouds<Product>
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

        public DataResult<Product> Get(string sqlQuery)
        {
            RDSConnection connection = new RDSConnection();
            connection.sqlConnection.Open();
            if (connection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataReader baglayici;
                MySqlCommand komut = new MySqlCommand();
                //string sqlsorgusu = "SELECT * FROM Warehouse.Products Where ID='" + Id + "'";
                komut.CommandText = sqlQuery;
                komut.Connection = connection.sqlConnection;
                baglayici = komut.ExecuteReader();
                if (baglayici.Read())
                {
                    Product product = new Product();
                    product.Id = (int)baglayici["Id"];
                    product.WarehouseID = (int)baglayici["WarehouseID"];
                    product.CategoryId = (int)baglayici["CategoryID"];
                    product.ProductName = (string)baglayici["ProductName"];
                    product.UnitsInStock = (int)baglayici["UnitsInStock"];
                    product.UnitPrice = (double)baglayici["UnitPrice"];
                    product.Description = (string)baglayici["Description"];

                    connection.sqlConnection.Close();
                    return new SuccessDataResult<Product>(product, Message.succces);
                }
            }
            connection.sqlConnection.Close();
            return new ErrorDataResult<Product>(Message.Error);
        }

        public DataResult<ObservableCollection<Product>> GetAll()
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


            return new SuccessDataResult<ObservableCollection<Product>>(Products, result.Message);
        }

        public Result Update(Product entity)
        {
            string sqlQuery = "UPDATE Warehouse.Products SET ID='" + entity.Id + "',WarehouseID='" + entity.WarehouseID + "',CategoryID='" + entity.CategoryId + "',ProductName='" + entity.ProductName + "',UnitsInStock='" + entity.UnitsInStock + "',UnitPrice='" + entity.UnitPrice+ "',Description='" + entity.Description + "' WHERE ID='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
