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

        // POST api/login
        public String Get([FromUri]string UserName, [FromUri]string Password)
        {
            string token = userDao.getUserFromLogin(UserName, Password);
            return token;
        }
    }
}
