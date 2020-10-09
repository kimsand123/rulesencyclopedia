using MySql.Data.MySqlClient;
using rulesencyclopedia;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Razor.Text;

namespace rulesencyclopediabackend
{
    public class UserDAO
    {
        ExceptionHandling exHandler = new ExceptionHandling();
        List<UserDTO> userList = new List<UserDTO>();
        Connection conn = new Connection();
        public UserDAO()
        {
        }
        public List<User> getUserList()
        {
            List<User> userList=null;
            try
            {
                using(var context = new rulesencyclopediaDBEntities1())
                {
                    userList = context.User.ToList();
                }
            }catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when getting all the users");
            }

          /*  MySqlCommand cmd = new MySqlCommand();
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
            }*/
            return userList;
        }

        public User getUser(int ID)
        {
            User user=null;
            try
            {
                using(var context = new rulesencyclopediaDBEntities1())
                {
                    user = context.User.Single(element => element.Id == ID);    
                }
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting user");
            }

            /*MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select  * FROM user WHERE ID=?ID";
            cmd.Parameters.AddWithValue("?ID", ID);
            List<String> result = conn.executeSqlQuery(cmd);
            UserDTO user = new UserDTO(result);*/
            return user;
        }

        public void postUser(User user)
        {
            try
            {
                using (var context = new rulesencyclopediaDBEntities1())
                {
                    //getting back the key for the created user.
                    User result = context.User.Add(user);
                    context.SaveChanges();
                }
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when creating new user");
            }

           /* MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Insert into user (FirstName, MiddleName, LastName, UserName, Password, Date) " +
                "Values (?FirstName , ?MiddleName, ?LastName, ?UserName, ?Password, ?Date)";
            cmd.Parameters.AddWithValue("?FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("?MiddleName", user.MiddleName);
            cmd.Parameters.AddWithValue("?LastName", user.LastName);
            cmd.Parameters.AddWithValue("?UserName", user.UserName);
            cmd.Parameters.AddWithValue("?Password", user.Password);
            cmd.Parameters.AddWithValue("?Date", user.Date);
            conn.executeSqlWrite(cmd);*/
        }

        public void editUser(int ID, User alteredUser)
        {
            using (var context = new rulesencyclopediaDBEntities1())
            {
                var user = context.User.First(a => a.Id == ID);
                user.FirstName = alteredUser.FirstName;
                user.MiddleName = alteredUser.MiddleName;
                user.LastName = alteredUser.LastName;
                user.UserName = alteredUser.UserName;
                user.Password = alteredUser.Password;
                user.Date = alteredUser.Date;
                context.SaveChanges();
            }
            
            
            /*   MySqlCommand cmd = new MySqlCommand();

               cmd.CommandText = "UPDATE user SET " +
                   "FirstName=Helge, " +
                   "MiddleName=Hansen, " +
                   "LastName=Pritmaskine, " +
                   "UserName=hehapri, " +
                   "Password=1234, " +
                   "WHERE ID=3";

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
               cmd.Parameters.AddWithValue("@Date", user.Date);

               cmd.CommandText = "UPDATE user SET FirstName=" + user.FirstName + ", MiddleName=" + user.MiddleName + ", LastName=" + user.LastName + ", Password=" + user.Password;

                   //Console.WriteLine("sql command string: " + cmd.CommandText);
               conn.executeSqlWrite(cmd);
               }*/
        }

        public void deleteUser(int ID)
        {
            try
            {
                using(var context = new rulesencyclopediaDBEntities1())
                {
                    var user = new User { Id = ID };
                    context.User.Attach(user);
                    context.User.Remove(user);
                    context.SaveChanges();
                }
            }catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong while deleting the user");
            }
        }
    }
}