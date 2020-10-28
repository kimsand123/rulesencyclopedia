using Newtonsoft.Json;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using rulesencyclopediabackend.Auth;

namespace rulesencyclopediabackend.Controllers
{
    public class GetFullGameController : ApiController
    {
        GetFullGameDAO getTheGame = new GetFullGameDAO();

        // GET: api/GetFullGame/5
        [BasicAuthentication]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            FullGameDTO fullGame = new FullGameDTO();
            fullGame = getTheGame.getTheGame(id);
            response = Request.CreateResponse(HttpStatusCode.OK, fullGame);
            return response;
        }

        // DELETE: api/GetFullGame/5
        public void Delete(int id)
        {
        }
    }
}
