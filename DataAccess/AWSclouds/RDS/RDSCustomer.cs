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

        public DataResult<Customer> Get(Func<Customer, bool> filter = null)
        {
            return filter == null ? new SuccessDataResult<Customer>() :
               new SuccessDataResult<Customer>(GetAll().Data.Single(filter));
        }

        public DataResult<ObservableCollection<Customer>> GetAll(Func<Customer, bool> filter = null)
        {
            string sqlQuery = "SELECT* FROM Warehouse.Customers ";
            DataResult<DataTable> result = RDSBase.Get(sqlQuery);

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            foreach (DataRow dataRow in result.Data.Rows)
            {
                Customer customer = new Customer();
                customer.Id = (int)dataRow["ID"];
                customer.CompanyName = (string)dataRow["CompanyName"];
                customers.Add(customer);
            }
            return filter == null ? new SuccessDataResult<ObservableCollection<Customer>>(customers) :
               new SuccessDataResult<ObservableCollection<Customer>>((ObservableCollection<Customer>)customers.Where(filter));
        }

        public Result Update(Customer entity)
        {
            string sqlQuery = "UPDATE Warehouse.Customers SET ID='" + entity.Id + "',CompanyName='" + entity.CompanyName + "' WHERE ID='" + entity.Id + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
