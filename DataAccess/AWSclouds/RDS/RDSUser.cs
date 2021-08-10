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
    public class RDSUser :IAWSclouds<User>
    {
        public Result Add(User entity)
        {
            
            string sqlQuery = "INSERT INTO Warehouse.Users(CustomerID,FirstName,LastName,Email,PasswordSalt,PasswordHash,Password) VALUES('" + entity.CustomerId + "','" + entity.FirstName + "','" + entity.LastName + "','" + entity.Email + "','" + entity.PasswordSalt + "','" + entity.PasswordHash + "','" + entity.Password + "')";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public Result Delete(User entity)
        {
            string sqlQuery = "DELETE FROM Warehouse.Users WHERE Id='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public DataResult<User> Get(Func<User, bool> filter = null)
        {
            return filter == null ? new SuccessDataResult<User>() :
                new SuccessDataResult<User>(GetAll().Data.Single(filter));
        }
        public DataResult<ObservableCollection<User>> GetAll(Func<User, bool> filter = null)
        {
            string sqlQuery = "SELECT* FROM Warehouse.Users ";
            DataResult<DataTable> result = RDSBase.Get(sqlQuery);

            ObservableCollection<User> Users = new ObservableCollection<User>();
            foreach (DataRow dataRow in result.Data.Rows)
            {
                User user = new User();
                user.Id = (int)dataRow["ID"];
                user.CustomerId = (int)dataRow["CustomerID"];
                user.FirstName = (string)dataRow["FirstName"];
                user.LastName = (string)dataRow["LastName"];
                user.Email = (string)dataRow["Email"];
                user.PasswordHash = (byte[])dataRow["PasswordHash"];
                user.PasswordSalt = (byte[])dataRow["PasswordSalt"];
                user.Password = dataRow["Password"].ToString();
                Users.Add(user);
            }

            return filter == null ? new SuccessDataResult<ObservableCollection<User>>(Users) :
               new SuccessDataResult<ObservableCollection<User>>((ObservableCollection<User>)Users.Where(filter));
        }

        public Result Update(User entity)
        {
            string sqlQuery = "UPDATE Warehouse.Users SET CustomerID='"+entity.CustomerId+"',FirstName='"+entity.FirstName+"',LastName='" + entity.LastName+"',Email='" + entity.Email+ "',PasswordHash='" + entity.PasswordHash+"',PasswordSalt='" + entity.PasswordSalt + "',Password='" + entity.Password + "' WHERE ID ='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }

}
