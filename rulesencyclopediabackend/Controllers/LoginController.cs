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
        HttpResponseMessage response = null;
        // POST api/login
        public HttpResponseMessage Get([FromUri]string UserName, [FromUri]string Password)
        {
            string token = userDao.getUserFromLogin(UserName, Password);
            if (token != "")
            {
                response = Request.CreateResponse(HttpStatusCode.OK, token);
            } else
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return response;
        }
    }
}
