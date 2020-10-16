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

namespace rulesencyclopediabackend.Controllers
{
    public class GameController : ApiController
    {
        ExceptionHandling exHandler = new ExceptionHandling();
        GameDAO dao = new GameDAO();
        // GET: api/Game
        public string Get()
        {
            string responseJson = "";

            List<Game> gameList = dao.getGameList();
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                responseJson = JsonConvert.SerializeObject(gameList, Formatting.Indented, serializerSettings);
                //responseJson = JsonSerializer.Serialize(gameList);
            } catch (JsonSerializationException ex)
            {
                exHandler.exceptionHandlerJson(ex, "cannot serialize the gamelist");
            }
            return responseJson;
        }

        // GET: api/Game/5
        public string Get(int id)
        {

            Game game = dao.getGame(id);
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string responseJson = JsonConvert.SerializeObject(game, Formatting.Indented, serializerSettings);
            return responseJson;
        }

        // POST: api/Game
        public void Post([FromBody]Game game)
        {
            dao.postGame(game);
        }

        // PUT: api/Game/5
        public void Put(int id, [FromBody]Game game)
        {
            dao.editGame(id, game);
        }

        // DELETE: api/Game/5
        public void Delete(int id)
        {
            dao.deleteGame(id);
        }
    }
}
