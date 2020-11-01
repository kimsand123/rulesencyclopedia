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
            user = (UserDTO)DTOConverter.Converter(user, userDao.checkUserName(UserName));
            HashAndSalt pwSecurity = new HashAndSalt();
            if (pwSecurity.AreEqual(Password, user.Password, user.Salt))
            {
                string token = userDao.getUserFromLogin(UserName, user.Password);
                if (token != "")
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, token);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Forbidden, "Password is wrong. Try again");
            }
            return response;
        }
    }
}
