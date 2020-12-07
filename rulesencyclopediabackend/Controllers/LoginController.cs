using rulesencyclopediabackend.Auth;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class LoginController : ApiController
    {
        UserDAO userDao = new UserDAO();
        ConvertToDTO DTOConverter = new ConvertToDTO();
        HttpResponseMessage response = null;
        // GET api/login?UserName=pepepe&Password=1234
        public HttpResponseMessage Get([FromUri]string UserName, [FromUri]string Password)
        { 
            //Check if username exists
            var dbUser = userDao.checkUserName(UserName);
            //If it does
            if (dbUser != null)
            {
                //check if the password when hashed and salted is equal to the password for the user from the db
                HashAndSalt pwSecurity = new HashAndSalt();
                if (pwSecurity.AreEqual(Password, dbUser.Password, dbUser.Salt))
                {
                    //transfer the db object values to a UserDTO object
                    UserDTO user = (UserDTO)DTOConverter.Converter(new UserDTO(), dbUser);
                    TokenDTO token = CheckToken.Instance.userLogin(user);
                   // string token = userDao.getUserFromLogin(UserName, user.Password);
                    if (token != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, token.token);
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Problem creating token");
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.Forbidden, "Password is wrong. Try again");
                }
            } else
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent, "User does not exist");
            }

            return response;
        }
    }
}
