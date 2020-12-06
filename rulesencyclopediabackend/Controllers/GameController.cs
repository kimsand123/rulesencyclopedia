using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using System.Text.Json;
using System.Text.Json.Serialization;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Exceptions;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using rulesencyclopediabackend.Auth;

namespace rulesencyclopediabackend.Controllers
{
    public class GameController : ApiController
    {
        DALExceptionHandling exHandler = new DALExceptionHandling();

        GameDAO dao = new GameDAO();

        ConvertToDTO DTOConverter = new ConvertToDTO();
        // GET: api/Game
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<GameDTO> gameList = dao.getGameList();
            //response = controlExcptHand.getResponseMessage(gameList);
            response = Request.CreateResponse(HttpStatusCode.OK, gameList);       
            return response;
        }

        // GET: api/Game/5
        [BasicAuthentication]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            GameDTO game = dao.getGame(id);
            return response;
        }

        // POST: api/Game
        [BasicAuthentication]
        public void Post([FromBody]Game game)
        {
            dao.postGame(game);
        }

        // PUT: api/Game/5
        [BasicAuthentication]
        public void Put(int id, [FromBody]Game game)
        {
            dao.editGame(id, game);
        }

        // DELETE: api/Game/5
        [BasicAuthentication]
        public void Delete(int id)
        {
            dao.deleteGame(id);
        }
    }
}
