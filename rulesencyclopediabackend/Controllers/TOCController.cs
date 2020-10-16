using Newtonsoft.Json;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web.Http;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace rulesencyclopediabackend.Controllers
{
    public class TOCController : ApiController
    {
        ExceptionHandling exHandler = new ExceptionHandling();
        TOCDAO dao = new TOCDAO();
        // GET: api/TOC
        public string Get([FromBody] TOCDTOFromView tocData)
        {
            string responseJson = "";

            List<TOC> tocList = dao.getTOCList(tocData.gameID);
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                responseJson = JsonConvert.SerializeObject(tocList, Formatting.Indented, serializerSettings);

            }
            catch (JsonSerializationException ex)
            {
                exHandler.exceptionHandlerJson(ex, "cannot serialize the tocList");
            }
            return responseJson;
        }

        // GET: api/TOC/5
        public string Get(int ID)
        {
            TOC toc = dao.getTOC(ID);
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string responseJson = JsonConvert.SerializeObject(toc, Formatting.Indented, serializerSettings);

            return responseJson;
        }

        // POST: api/TOC
        public void Post([FromBody]TOC toc)
        {
            dao.postTOC(toc);
        }

        // PUT: api/TOC/5
        public void Put(int id, [FromBody]TOC toc)
        {
            dao.editTOC(id, toc);
        }

        // DELETE: api/TOC/5
        public void Delete(int id)
        {
            dao.deleteTOC(id);
        }
    }
}
