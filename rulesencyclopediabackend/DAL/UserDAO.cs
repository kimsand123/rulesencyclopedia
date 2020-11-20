﻿using MySqlX.XDevAPI.Common;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
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

        internal User checkUserName(string userName)
        {
            User user = null;
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    user = context.User.Single(element => element.UserName == userName);
                }
            } catch (EntityException ex)
            { 
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when checking the username");
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }

            return user;
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

        internal int postUser(User user)
        {
            HashAndSalt pwSecurity = new HashAndSalt();
            int result=-999999;
            string password = user.Password;
            string salt = pwSecurity.getSalt();
            string saltedPassword = pwSecurity.GenerateHash(password, salt, 0);
            password = "";  //ERASING IT FROM MEMORY
            user.Password = saltedPassword;
            user.Salt = salt;

            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    //getting back the key for the created user.
                    context.User.Add(user);
                    result = context.SaveChanges();
                }
            } catch (DbEntityValidationException ex)
            {
            }
            return result;
        }

        internal int editUser(int ID, User alteredUser)
        {
            int result = -999999;
            var context = new rulesencyclopediaDBEntities1();           
            var user = context.User.First(a => a.Id == ID);
            user.FirstName = alteredUser.FirstName;
            user.MiddleName = alteredUser.MiddleName;
            user.LastName = alteredUser.LastName;
            user.UserName = alteredUser.UserName;
            user.Password = alteredUser.Password;
            user.Date = alteredUser.Date;
            try
            {
                result = context.SaveChanges();
            } catch (EntityException e)
            {
                //needs to test errors
            }
            return result;
        }

        internal int deleteUser(int ID)
        {
            int result = -999999;
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    var user = new User { Id = ID };
                    context.User.Attach(user);
                    context.User.Remove(user);
                    result = context.SaveChanges();
                }
            }catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong while deleting the user");
            }
            return result;
        }

        internal string getUserFromLogin(string userName, string password)
        {
            User user = null;
            UserDTO userDto = null;
            TokenDTO token = new TokenDTO();
            token.token = "";

            try
            {
                var context = new rulesencyclopediaDBEntities1();
                user = context.User.Single(element => element.UserName == userName && element.Password==password);
                if (user != null)
                {
                    //Using DTOConverter to convert entity return object to dto.
                    userDto = (UserDTO)DTOConverter.Converter(new UserDTO(), user);
                    token = CheckToken.Instance.userLogin(userDto);
                }
                else
                {
                    //exception handling...
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting user");
            } 
            catch(InvalidOperationException ex)
            {

            }
            return token.token;
        }
    }
}