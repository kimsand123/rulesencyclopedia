using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
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
        ConvertToDTO DTOConverter = new ConvertToDTO();
        public UserDAO()
        {
        }
        internal List<User> getUserList()
        {
            List<User> userList=null;
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    userList = context.User.ToList();
                }
            }catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when getting all the users");
            }
            return userList;
        }

        internal User getUser(int ID)
        {
            User user=null;
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    user = context.User.Single(element => element.Id == ID);    
                }
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting user");
            }
            return user;
        }

        internal void postUser(User user)
        {
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    //getting back the key for the created user.
                    User result = context.User.Add(user);
                    context.SaveChanges();
                }
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when creating new user");
            }
        }

        internal void editUser(int ID, User alteredUser)
        {
            var context = new rulesencyclopediaDBEntities1();
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
        }

        internal void deleteUser(int ID)
        {
            try
            {
                var context = new rulesencyclopediaDBEntities1();
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

        internal string getUserFromLogin(string userName, string password)
        {
            User user = null;
            UserDTO userDto = null;
            TokenDTO token = null;
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    user = context.User.Single(element => element.UserName == userName && element.Password==password);
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting user");
            }

            if (user!= null)
            {
                userDto = (UserDTO)DTOConverter.Converter(new UserDTO(), user);
                token = CheckToken.Instance.userLogin(userDto);
            }
            return token.token;
        }
    }
}