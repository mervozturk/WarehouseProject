using Core.Messages;
using Core.Results;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.AWSclouds.RDS
{
    public class RDSBase
    {
        public static Result Crud(string sqlQuery)
        {
            RDSConnection connection = new RDSConnection();
            connection.sqlConnection.Open();
            if (connection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                komut.Connection = connection.sqlConnection;
                komut.CommandText = sqlQuery;
                komut.ExecuteNonQuery();
                return new SuccessResult(Message.succces);

            }
            connection.sqlConnection.Close();
            return new ErrorResult(Message.Error);
        }

        public static DataResult<DataTable> Get(string sqlQuery)
        {
            RDSConnection connection = new RDSConnection();
            connection.sqlConnection.Open();
            if (connection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand komut = new MySqlCommand();
               
                DataTable dataTable = new DataTable();

                komut.CommandText = sqlQuery;
                komut.Connection = connection.sqlConnection;
                dataAdapter.SelectCommand = komut;
                dataAdapter.Fill(dataTable);
                return new SuccessDataResult<DataTable>(dataTable, Message.succces);
            }
            connection.sqlConnection.Close();
            return new ErrorDataResult<DataTable>(Message.Error);
        }
    }
}
