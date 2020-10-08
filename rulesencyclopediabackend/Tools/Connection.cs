using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using rulesencyclopediabackend.Exceptions;

namespace rulesencyclopediabackend.Tools
{
    public class Connection
    {
        ExceptionHandling exHandler = new ExceptionHandling();
        public List<String> executeSqlQuery(string sqlStatement)
        {
            MySqlConnection conn = databaseConnection();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = sqlStatement;
            MySqlDataReader reader = null;
            List<String> result = new List<String>();
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int element = 0; element < reader.FieldCount; element++)
                    {
                        result.Add(reader.GetValue(element).ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                exHandler.exceptionHandlerMySql(ex, sqlStatement);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int executeSqlPost(MySqlCommand cmd)
        {
            MySqlConnection conn = databaseConnection();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                exHandler.exceptionHandlerMySql(ex, cmd.CommandText);
            }
            finally
            {
                conn.Close();
            }
            //TODO not implemented return value for post perhaps it is the error.
            return 1;
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "server=localhost;database=rulesencyclopedia;uid=root;pwd=dtupassword";
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
            }
            catch (MySqlException ex)
            {
                exHandler.exceptionHandlerMySql(ex, connectionString);
            }
            return connection;
        }

        public int executeSqlInsert(MySqlCommand cmd)
        {
            MySqlConnection conn = databaseConnection();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                exHandler.exceptionHandlerMySql(ex, cmd.CommandText);
            }
            finally
            {
                conn.Close();
            }
            //TODO not implemented return value for post perhaps it is the error.
            return 1;
        }
    }
}