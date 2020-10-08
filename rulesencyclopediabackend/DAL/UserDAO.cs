using MySql.Data.MySqlClient;
using rulesencyclopedia;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace rulesencyclopediabackend
{
    public class UserDAO
    {
        List<UserDTO> userList = new List<UserDTO>();
        Connection conn = new Connection();
        public UserDAO()
        {
        }

        public List<UserDTO> getUserList()
        {
            String sqlStatement = "Select * FROM user";
            List<String> result;
            result = conn.executeSqlQuery(sqlStatement);
            int numberOfProperties = typeof(UserDTO).GetProperties().Count();
            int numberOfData = result.Count();
            int numberOfObjects = numberOfData / numberOfProperties;
            List<UserDTO> userList= new List<UserDTO>();
            List<String> valueList= new List<String>();
            //Generate the String list for each UserDTO, instantiate the DTO and add it to the list of DTO's
            for (int dataCounter = 0; dataCounter < numberOfData; dataCounter++)
            {
                valueList.Add(result[dataCounter].ToString());
                if (dataCounter == 6 || (dataCounter-6)%7==0)
                {
                    UserDTO user = new UserDTO(valueList);
                    valueList.Clear();
                    userList.Add(user);
                }
            }
            return userList;
        }

        public UserDTO getUser(int id)
        {
            String sqlStatement = "Select  * FROM user WHERE ID="+id;
            List<String> result;
            result = conn.executeSqlQuery(sqlStatement);
            UserDTO user = new UserDTO(result);
            return user;
        }

        public void postUser(UserDTO user)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Insert into user (FirstName, MiddleName, LastName, UserName, Password, Date) " +
                "Values (?FirstName , ?MiddleName, ?LastName, ?UserName, ?Password, ?Date)";
            cmd.Parameters.Add("?FirstName", MySqlDbType.VarChar).Value = user.FirstName;
            cmd.Parameters.Add("?MiddleName", MySqlDbType.VarChar).Value = user.MiddleName;
            cmd.Parameters.Add("?LastName", MySqlDbType.VarChar).Value = user.LastName;
            cmd.Parameters.Add("?UserName", MySqlDbType.VarChar).Value = user.UserName;
            cmd.Parameters.Add("?Password", MySqlDbType.VarChar).Value = user.Password;
            cmd.Parameters.Add("?Date", MySqlDbType.DateTime).Value = user.Date;
            conn.executeSqlWrite(cmd);
        }

        public void editUser(int ID, UserDTO user)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Update user Set " +
                "FirstName=?FirstName, " +
                "MiddleName=?MiddleName, " +
                "LastName=?LastName, " +
                "UserName=?UserName, " +
                "Password=?Password, " +
                "Date=?Date, " +
                "Where ID=?ID";
            cmd.Parameters.Add("?ID", MySqlDbType.Int16).Value = Convert.ToInt16(ID);
            cmd.Parameters.Add("?FirstName", MySqlDbType.VarChar).Value = user.FirstName;
            cmd.Parameters.Add("?MiddleName", MySqlDbType.VarChar).Value = user.MiddleName;
            cmd.Parameters.Add("?LastName", MySqlDbType.VarChar).Value = user.LastName;
            cmd.Parameters.Add("?UserName", MySqlDbType.VarChar).Value = user.UserName;
            cmd.Parameters.Add("?Password", MySqlDbType.VarChar).Value = user.Password;
            cmd.Parameters.Add("?Date", MySqlDbType.DateTime).Value = user.Date;

            conn.executeSqlWrite(cmd);
        }
    }
}