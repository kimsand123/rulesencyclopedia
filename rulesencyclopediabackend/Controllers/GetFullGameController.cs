using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class GetFullGameController : ApiController
    {
        // GET: api/GetFullGame/5
        public string Get(int id)
        {
            return "value";
        }

        // DELETE: api/GetFullGame/5
        public void Delete(int id)
        {
        }
    }
}
