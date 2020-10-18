
using Newtonsoft.Json;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace rulesencyclopediabackend.Controllers
{
    public class EntryController : ApiController
    {

 
        ExceptionHandling exHandler = new ExceptionHandling();
        EntryDAO dao = new EntryDAO();

        // GET: api/Entry
        public HttpResponseMessage Get([FromBody] TOCDTOFromView tocData)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<Entry> entryList = dao.getEntriesForToc(tocData.tocID);
            try
            {
                //For at håndtere cirkulære referencer der gjorde at der kom uendeligt antal objekter.
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string responseJson = JsonConvert.SerializeObject(entryList, Formatting.Indented, serializerSettings);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);
            }
            catch (JsonSerializationException ex)
            {
                // TODO: write ex to logfile.
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the entry list");
            }
            return response;
        }


        // GET: api/Entry/5
        public HttpResponseMessage Get(int TOCID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Entry entry = dao.getEntry(TOCID);
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string responseJson = JsonConvert.SerializeObject(entry, Formatting.Indented, serializerSettings);
            } catch (JsonSerializationException ex)
            {
                // TODO: write ex to logfile.
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the entry");
            }
            return response;
        }

        // POST: api/Entry
        public void Post([FromBody]Entry entry)
        {
            dao.postEntry(entry);
        }

        // PUT: api/Entry/5
        public void Put(int id, [FromBody]Entry entry)
        {
            dao.editEntry(id, entry);
        }

        // DELETE: api/Entry/5
        public void Delete(int id)
        {
            dao.deleteEntry(id);
        }
    }
}
