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
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.AWSclouds.RDS
{
    public class RDSCategory :IAWSclouds<Category>
    {
        public Result Add(Category entity)
        {
            string sqlQuery = "INSERT INTO Warehouse.Categorys(CategoryID,CategoryName) VALUES('" + entity.CategoryId+ "','" + entity.CategoryName + "')";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public Result Delete(Category entity)
        {
            string sqlQuery = "DELETE FROM Warehouse.Categorys WHERE CategoryID='" + entity.CategoryId + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }


        public DataResult<Category> Get(Func<Category, bool> filter = null)
        {                
            return filter == null ? new SuccessDataResult<Category>() :
               new SuccessDataResult<Category>(GetAll().Data.Single(filter));
        }

        public DataResult<ObservableCollection<Category>> GetAll(Func<Category, bool> filter = null)
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
            return filter == null ? new SuccessDataResult<ObservableCollection<Category>>(categories) :
               new SuccessDataResult<ObservableCollection<Category>>((ObservableCollection<Category>)categories.Where(filter));

        }

        public Result Update(Category entity)
        {
            string sqlQuery = "UPDATE Warehouse.Categorys SET CategoryID='" + entity.CategoryId+ "',CategoryName='" + entity.CategoryName + "' WHERE CategoryID='" + entity.CategoryId + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
