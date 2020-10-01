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
        public List<String> executeSqlStatement(string sqlStatement)
        {
            MySqlConnection conn = databaseConnection();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = sqlStatement;
            MySqlDataReader reader = null;
            List<String> result = new List<String>();
            DataTable table = null;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                //Wait for the data to download.
                while (reader.Read()){}


                for (int element = 0; element < reader.FieldCount; element++)
                {

                    //result.Add("{"+reader.GetName(element)+":"+reader.GetValue(element)+"}");
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

        private MySqlConnection databaseConnection()
        {
            string connectionString = "server=localhost;database=rulesencyclopedia;uid=root;pwd=dtupassword";
            MySqlConnection connection=null;
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



        
    }
}