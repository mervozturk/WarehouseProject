using Core.Messages;
using Core.Results;
using DataAccess.Abstact;
using Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.AWSclouds.RDS
{
    public class RDSUser : IAWSclouds<User>
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

        public DataResult<User> Get(string sqlQuery)
        {
            RDSConnection connection = new RDSConnection();
            connection.sqlConnection.Open();
            if (connection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataReader baglayici;
                MySqlCommand komut = new MySqlCommand();
                komut.CommandText = sqlQuery;
                komut.Connection = connection.sqlConnection;
                baglayici = komut.ExecuteReader();
                if (baglayici.Read())
                {
                    User user = new User();
                    user.Id = (int)baglayici["ID"];
                    user.CustomerId = (int)baglayici["CustomerID"];
                    user.FirstName = baglayici["Firstname"].ToString();
                    user.LastName = baglayici["Lastname"].ToString();
                    user.Email = baglayici["Email"].ToString();
                    user.PasswordHash = (byte[])baglayici["PasswordHash"];
                    user.PasswordSalt = (byte[])baglayici["PasswordSalt"];
                    user.Password = (string)baglayici["Password"];
                    connection.sqlConnection.Close();
                    return new SuccessDataResult<User>(user,Message.succces);
                }
            }
            connection.sqlConnection.Close();
            return new ErrorDataResult<User>(Message.Error);
        }
       
        public DataResult<ObservableCollection<User>> GetAll(string sqlQuery)
        {
            //string sqlQuery = "SELECT* FROM Warehouse.Users ";
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
                user.Password = (string)dataRow["Password"];
                Users.Add(user);
            }


            return new SuccessDataResult<ObservableCollection<User>>(Users, result.Message);
        }

        public Result Update(User entity)
        {
            string sqlQuery = "UPDATE Warehouse.Users SET CustomerID='"+entity.CustomerId+"',FirstName='"+entity.FirstName+"',LastName='" + entity.LastName+"',Email='" + entity.Email+ "',PasswordHash='" + entity.PasswordHash+"',PasswordSalt='" + entity.PasswordSalt + "',Password='" + entity.Password + "' WHERE ID ='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }

}
