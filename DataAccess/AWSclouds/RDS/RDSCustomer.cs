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
    public class RDSCustomer : IAWSclouds<Customer>
    {
        public Result Add(Customer entity)
        {
            string sqlQuery = "INSERT INTO Warehouse.Customers(ID,CompanyName) VALUES('" + entity.Id + "','" + entity.CompanyName+ "')";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public Result Delete(Customer entity)
        {
            string sqlQuery = "DELETE FROM Warehouse.Customers WHERE Id='" + entity. Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public DataResult<Customer> Get(string sqlQuery)
        {
            RDSConnection connection = new RDSConnection();
            connection.sqlConnection.Open();
            if (connection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataReader baglayici;
                MySqlCommand komut = new MySqlCommand();
                //string sqlsorgusu = "SELECT * FROM Warehouse.Customers Where ID='" + Id + "'";
                komut.CommandText = sqlQuery;
                komut.Connection = connection.sqlConnection;
                baglayici = komut.ExecuteReader();
                if (baglayici.Read())
                {
                    Customer customer = new Customer();
                    customer.Id = (int)baglayici["ID"];
                    customer.CompanyName = baglayici["Companyname"].ToString();

                    connection.sqlConnection.Close();
                    return new SuccessDataResult<Customer>(customer, Message.succces);
                }
            }
            connection.sqlConnection.Close();
            return new ErrorDataResult<Customer>(Message.Error);
        }

        public DataResult<ObservableCollection<Customer>> GetAll(string sqlQuery)
        {
            //string sqlQuery = "SELECT* FROM Warehouse.Customers ";
            DataResult<DataTable> result = RDSBase.Get(sqlQuery);

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            foreach (DataRow dataRow in result.Data.Rows)
            {
                Customer customer = new Customer();
                customer.Id = (int)dataRow["ID"];
                customer.CompanyName = (string)dataRow["CompanyName"];
                customers.Add(customer);
            }


            return new SuccessDataResult<ObservableCollection<Customer>>(customers, result.Message);
        }

        public Result Update(Customer entity)
        {
            string sqlQuery = "UPDATE Warehouse.Customers SET ID='" + entity.Id + "',CompanyName='" + entity.CompanyName + "' WHERE ID='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
