using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace rulesencyclopediabackend.Controllers
{
    public class HomeController : ApiController
    {
        //For the client to check if the service is up.
        public HttpResponseMessage Index()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Service Up");
        }
    }
}
