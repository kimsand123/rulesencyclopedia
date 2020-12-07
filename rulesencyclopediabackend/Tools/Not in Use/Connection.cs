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
    //Database connection
    public class Connection
    {
        DALExceptionHandling exHandler = new DALExceptionHandling();

        //Executes an SQL query
        public List<String> executeSqlQuery(MySqlCommand cmd)
        {
            //Get the db connection
            MySqlConnection conn = databaseConnection();
            //Setting the connection in the cmd object
            cmd.Connection = conn;
            MySqlDataReader reader = null;
            List<String> result = new List<String>();

            //try to open the connection and execute the SQL command.
            try
            {
                conn.Open();
                //execute the select statement
                reader = cmd.ExecuteReader();
                //As long as there is data
                while (reader.Read())
                {
                    for (int element = 0; element < reader.FieldCount; element++)
                    {
                        //add the to the result list
                        result.Add(reader.GetValue(element).ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                exHandler.exceptionHandlerMsSql(ex, cmd.CommandText);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int executeSqlWrite(MySqlCommand cmd)
        {
            MySqlConnection conn = databaseConnection();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                //Execute the none select statement
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                exHandler.exceptionHandlerMsSql(ex, "Problem with the sqlStatement: " + cmd.CommandText);
            }
            finally
            {
                conn.Close();
            }
            //TODO not implemented return value for post perhaps it is the error.
            return 1;
        }

        public int executeSqlUpdate(MySqlCommand cmd)
        {
            MySqlConnection conn = databaseConnection();
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                exHandler.exceptionHandlerMsSql(ex, "Problem with the sqlStatement: " + cmd.CommandText);
            }
            finally
            {
                conn.Close();
            }
            //TODO not implemented return value for Update perhaps it is the error.
            return 1;
        }

        //Serving the database connection
        private MySqlConnection databaseConnection()
        {
            string connectionString = "server=localhost;database=rulesencyclopedia;uid=root;pwd=dtupassword";
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
            }
            catch (SqlException ex)
            {
                exHandler.exceptionHandlerMsSql(ex, "Could not connect to " +connectionString);
            }
            return connection;
        }
    }
}