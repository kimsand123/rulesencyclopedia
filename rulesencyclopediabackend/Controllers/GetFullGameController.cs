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
            try
            {
                string responseJson = JsonConvert.SerializeObject(fullGame);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);
            }
            catch (JsonSerializationException ex)
            {
                // TODO: write ex to logfile.
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the entry list");
            }
            finally
            {
                // TODO: close the logfile
            }
            return response;
        }

        // DELETE: api/GetFullGame/5
        public void Delete(int id)
        {
        }
    }
}
