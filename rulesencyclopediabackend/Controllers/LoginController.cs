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
        // POST api/login
        public HttpResponseMessage Get([FromUri]string UserName, [FromUri]string Password)
        {
            UserDTO user = new UserDTO();
            var dbUser = userDao.checkUserName(UserName);
            if (dbUser != null)
            {
                user = (UserDTO)DTOConverter.Converter(user, dbUser);
                HashAndSalt pwSecurity = new HashAndSalt();
                if (pwSecurity.AreEqual(Password, user.Password, user.Salt))
                {
                    string token = userDao.getUserFromLogin(UserName, user.Password);
                    if (token != "")
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, token);
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
