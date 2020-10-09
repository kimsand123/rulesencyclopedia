using MySql.Data.MySqlClient;
using rulesencyclopedia;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Razor.Text;

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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * From user";
            List<string> result = conn.executeSqlQuery(cmd);
            int numberOfData = result.Count();
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

        public UserDTO getUser(int ID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select  * FROM user WHERE ID=?ID";
            cmd.Parameters.AddWithValue("?ID", ID);
            List<String> result = conn.executeSqlQuery(cmd);
            UserDTO user = new UserDTO(result);
            return user;
        }

        public void postUser(UserDTO user)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Insert into user (FirstName, MiddleName, LastName, UserName, Password, Date) " +
                "Values (?FirstName , ?MiddleName, ?LastName, ?UserName, ?Password, ?Date)";
            cmd.Parameters.AddWithValue("?FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("?MiddleName", user.MiddleName);
            cmd.Parameters.AddWithValue("?LastName", user.LastName);
            cmd.Parameters.AddWithValue("?UserName", user.UserName);
            cmd.Parameters.AddWithValue("?Password", user.Password);
            cmd.Parameters.AddWithValue("?Date", user.Date);
            conn.executeSqlWrite(cmd);
        }

        public void editUser(int ID, UserDTO user)
        {
            MySqlCommand cmd = new MySqlCommand();

            /*cmd.CommandText = "UPDATE user SET " +
                "FirstName=Helge, " +
                "MiddleName=Hansen, " +
                "LastName=Pritmaskine, " +
                "UserName=hehapri, " +
                "Password=1234, " +
                "WHERE ID=3";*/

            //Checking if the object is the same as the id number from the adressline.
            if (ID == user.ID) { 

            /*cmd.CommandText = "UPDATE user SET " +

            "FirstName=@FirstName, " +
            "MiddleName=@MiddleName, " +
            "LastName=@LastName, " +
            "UserName=@UserName, " +
            "Password=@Password, " +
            "WHERE ID=@ID";

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", user.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Date", user.Date);*/

            cmd.CommandText = "UPDATE user SET FirstName=" + user.FirstName + ", MiddleName=" + user.MiddleName + ", LastName=" + user.LastName + ", Password=" + user.Password;

                //Console.WriteLine("sql command string: " + cmd.CommandText);
            conn.executeSqlWrite(cmd);
            }
        }
    }
}