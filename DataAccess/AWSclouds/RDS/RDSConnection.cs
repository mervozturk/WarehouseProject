using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.AWSclouds.RDS
{
    public class RDSConnection
    {
        public MySqlConnection sqlConnection = new MySqlConnection("Server=warehouserds.crfzwpssveh6.us-east-2.rds.amazonaws.com;Database=Warehouse;Uid=WarehouseDB;Pwd=Warehouse1;");
    }
}
