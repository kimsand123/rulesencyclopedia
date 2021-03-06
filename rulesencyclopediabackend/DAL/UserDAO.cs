﻿using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace rulesencyclopediabackend
{
    public class UserDAO
    {
        ConvertToDTO DTOConverter = new ConvertToDTO();
        DALExceptionHandling exHandler = new DALExceptionHandling();
        public UserDAO()
        {
        }
        internal List<UserDTO> getUserList()
        {
            List<User> userList=null;
            List<UserDTO> userDTOs = null;
            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            try
            {
                userList = context.User.ToList();
                userDTOs = new List<UserDTO>();
                foreach (User user in userList)
                {
                    UserDTO userDTO = (UserDTO)DTOConverter.Converter(new UserDTO(), user);
                    userDTOs.Add(userDTO);
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when getting all the users");
            }
            finally
            {
                context.Dispose();
            }
            return userDTOs;
        }

        internal UserDTO checkUserName(string userName)
        {
            User user = null;
            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            try
            {             
                user = context.User.Single(element => element.UserName == userName);
            } catch (EntityException ex)
            { 
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when checking the username");
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
            finally
            {
                context.Dispose();
            }
            UserDTO userDTO = (UserDTO)DTOConverter.Converter(new UserDTO(), user);
            return userDTO;
        }

        internal UserDTO getUser(int ID)
        {
            User user=null;
            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            try
            {
                user = context.User.Single(element => element.Id == ID);    
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting user");
            }
            finally
            {
                context.Dispose();
            }
            UserDTO userDTO = (UserDTO)DTOConverter.Converter(new UserDTO(), user);
            return userDTO;
        }

        internal int postUser(User user)
        {
            HashAndSalt pwSecurity = new HashAndSalt();
            int result=-999999;
            string password = user.Password;
            string salt = pwSecurity.getSalt();
            //.GenerateHash(password, salt, 0) the last 0 is the starting value for the recursive iteration counter
            string saltedPassword = pwSecurity.GenerateHash(password, salt, 0);
            password = "";  //ERASING IT FROM MEMORY
            user.Password = saltedPassword;
            user.Salt = salt;

            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            try
            {
                //getting back the key for the created user.
                context.User.Add(user);
                result = context.SaveChanges();
            } 
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when posting user");
            }
            finally
            {
                context.Dispose();
            }
            return result;
        }

        internal int editUser(User alteredUser)
        {
            int result = -999999;
            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            var user = context.User.First(a => a.Id == alteredUser.Id);
            user.FirstName = alteredUser.FirstName;
            user.MiddleName = alteredUser.MiddleName;
            user.LastName = alteredUser.LastName;
            user.UserName = alteredUser.UserName;
            user.Password = alteredUser.Password;
            user.Date = alteredUser.Date;
            try
            {
                result = context.SaveChanges();
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when editing user");
            }
            finally
            {
                context.Dispose();
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
    }
}