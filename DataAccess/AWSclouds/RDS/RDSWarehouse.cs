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
    public class RDSWarehouse : IAWSclouds<Warehouse>
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

        public DataResult<Warehouse> Get(string sqlQuery)
        {
            RDSConnection connection = new RDSConnection();
            connection.sqlConnection.Open();
            if (connection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataReader baglayici;
                MySqlCommand komut = new MySqlCommand();
                //string sqlsorgusu = "SELECT * FROM Warehouse.Warehouses Where ID='" + Id + "'";
                komut.CommandText = sqlQuery;
                komut.Connection = connection.sqlConnection;
                baglayici = komut.ExecuteReader();
                if (baglayici.Read())
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse.WarehouseId = (int)baglayici["WarehouseID"];
                    warehouse.CustomerID = (int)baglayici["CustomerID"];
                    warehouse.WarehouseName = baglayici["Warehousename"].ToString();

                    connection.sqlConnection.Close();
                    return new SuccessDataResult<Warehouse>(warehouse, Message.succces);
                }
            }
            connection.sqlConnection.Close();
            return new ErrorDataResult<Warehouse>(Message.Error);
        }

        public DataResult<ObservableCollection<Warehouse>> GetAll()
        {
            string sqlQuery = "SELECT* FROM Warehouse.Warehouses ";
            DataResult<DataTable> result = RDSBase.Get(sqlQuery);

            ObservableCollection<Warehouse> warehouses= new ObservableCollection<Warehouse>();
            foreach (DataRow dataRow in result.Data.Rows)
            {
                Warehouse warehouse = new Warehouse();
                warehouse.WarehouseId = (int)dataRow["WarehouseID"];
                warehouse.CustomerID = (int)dataRow["CustomerID"];
                warehouse.WarehouseName = (string)dataRow["WarehouseName"];
                warehouses.Add(warehouse);
            }


            return new SuccessDataResult<ObservableCollection<Warehouse>>(warehouses, result.Message);
        }

        public Result Update(Warehouse entity)
        {
            string sqlQuery = "UPDATE Warehouse.Warehouses SET WarehouseID='" + entity.WarehouseId + "',CustomerID='" + entity.CustomerID + "',WarehouseName='" + entity.WarehouseName + "' WHERE WarehouseID='" + entity.WarehouseId + "'";
            Result result = RDSBase.Crud(sqlQuery);
            return result;
        }
    }
}
