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
    public class RDSWarehouse  : IAWSclouds<Warehouse>
    {
        public Result Add(Warehouse entity)
        {
            string sqlQuery = "INSERT INTO Warehouse.Warehouses(WarehouseID,CustomerID,WarehouseName) VALUES('" + entity.WarehouseId + "','" + entity.CustomerID + "','" + entity.WarehouseName + "')";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public Result Delete(Warehouse entity)
        {
            string sqlQuery = "DELETE FROM Warehouse.Warehouses WHERE WarehouseID='" + entity.WarehouseId + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }

        public DataResult<Warehouse> Get(Func<Warehouse, bool> filter = null)
        {
            return filter == null ? new SuccessDataResult<Warehouse>() :
               new SuccessDataResult<Warehouse>(GetAll().Data.Single(filter));
        }

        public DataResult<ObservableCollection<Warehouse>> GetAll(Func<Warehouse, bool> filter = null)
        {
            string sqlQuery = "SELECT* FROM Warehouse.Warehouses";
            DataResult<DataTable> result = RDSBase.Get(sqlQuery);

            ObservableCollection<Warehouse> warehouses = new ObservableCollection<Warehouse>();
            foreach (DataRow dataRow in result.Data.Rows)
            {
                Warehouse warehouse = new Warehouse();
                warehouse.WarehouseId = (int)dataRow["WarehouseID"];
                warehouse.CustomerID = (int)dataRow["CustomerID"];
                warehouse.WarehouseName = (string)dataRow["WarehouseName"];
                warehouses.Add(warehouse);
            }

            return filter == null ? new SuccessDataResult<ObservableCollection<Warehouse>>(warehouses) :
               new SuccessDataResult<ObservableCollection<Warehouse>>(new ObservableCollection<Warehouse>(warehouses.Where(filter)));
        }

        public Result Update(Warehouse entity)
        {
            string sqlQuery = "UPDATE Warehouse.Warehouses SET WarehouseID='" + entity.WarehouseId + "',CustomerID='" + entity.CustomerID + "',WarehouseName='" + entity.WarehouseName + "' WHERE WarehouseID='" + entity.WarehouseId + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
