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

namespace rulesencyclopediabackend.Controllers
{
    public class GetFullGameController : ApiController
    {
        GetFullGameDAO getTheGame = new GetFullGameDAO();

        // GET: api/GetFullGame/5
        public HttpResponseMessage Get([FromBody] string Authentication, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (CheckToken.Instance.doCheckToken(Authentication))
            {

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

            } else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Your token is not Valid, Login again or create a user");
            }
            return response;
        }

        // DELETE: api/GetFullGame/5
        public void Delete(int id)
        {
        }
    }
}
