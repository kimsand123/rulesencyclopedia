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
        // GET api/login
        public String Post([FromBody]string UserName, [FromBody]string Password)
        {
            string token = "123456789";
            return token;
        }
    }
}
