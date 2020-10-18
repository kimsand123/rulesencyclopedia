using Newtonsoft.Json;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class EntryController : ApiController
    {
        ExceptionHandling exHandler = new ExceptionHandling();
        EntryDAO dao = new EntryDAO();
        // GET: api/Entry
        public string Get([FromBody] TOCDTOFromView tocData)
        {
            string responseJson = "";
            
            List<Entry> entryList = dao.getEntriesForToc(tocData.tocID);
            try
            {
                //For at håndtere cirkulære referencer der gjorde at der kom uendeligt antal objekter.
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                responseJson = JsonConvert.SerializeObject(entryList, Formatting.Indented, serializerSettings);
            }
            catch (JsonSerializationException ex)
            {
                exHandler.exceptionHandlerJson(ex, "cannot serialize the entryList");
            }
            return responseJson;
        }


        // GET: api/Entry/5
        public string Get(int TOCID)
        {
            Entry entry = dao.getEntry(TOCID);
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string responseJson = JsonConvert.SerializeObject(entry, Formatting.Indented, serializerSettings);
            return responseJson;
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
