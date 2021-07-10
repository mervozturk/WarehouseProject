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
    public class RDSCategory : IAWSclouds<Category>
    {
        public Result Add(Category entity)
        {
            string sqlQuery = "INSERT INTO Warehouse.Categorys(CategoryID,CategoryName) VALUES('" + entity.CategoryId+ "','" + entity.CategoryName + "')";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public Result Delete(Category entity)
        {
            string sqlQuery = "DELETE FROM Warehouse.Categorys WHERE Id='" + entity.CategoryId + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public DataResult<Category> Get(string sqlQuery)
        {
            RDSConnection connection = new RDSConnection();
            connection.sqlConnection.Open();
            if (connection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataReader baglayici;
                MySqlCommand komut = new MySqlCommand();
                //string sqlsorgusu = "SELECT * FROM Warehouse.Categorys Where ID='" + Id + "'";
                komut.CommandText = sqlQuery;
                komut.Connection = connection.sqlConnection;
                baglayici = komut.ExecuteReader();
                if (baglayici.Read())
                {
                    Category category = new Category();
                    category.CategoryId = (int)baglayici["CategoryID"];  
                    category.CategoryName= baglayici["Categoryname"].ToString();

                    connection.sqlConnection.Close();
                    return new SuccessDataResult<Category>(category, Message.succces);
                }
            }
            connection.sqlConnection.Close();
            return new ErrorDataResult<Category>(Message.Error);
        }

        public DataResult<ObservableCollection<Category>> GetAll()
        {
            string sqlQuery = "SELECT* FROM Warehouse.Categorys ";
            DataResult<DataTable> result = RDSBase.Get(sqlQuery);

            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            foreach (DataRow dataRow in result.Data.Rows)
            {
                Category category = new Category();
                category.CategoryId = (int)dataRow["CategoryID"];
                category.CategoryName = (string)dataRow["CategoryName"];
               categories.Add(category);
            }


            return new SuccessDataResult<ObservableCollection<Category>>(categories, result.Message);
        }

        public Result Update(Category entity)
        {
            string sqlQuery = "UPDATE Warehouse.Categorys SET CategoryID='" + entity.CategoryId+ "',CategoryName='" + entity.CategoryName + "' WHERE CategoryID='" + entity.CategoryId + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
