using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Configuration;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;

namespace rulesencyclopediabackend.Controllers
{   
    public class EntryController : ApiController
    {
        EntryDAO dao = new EntryDAO();

        ConvertToDTO DTOConverter = new ConvertToDTO();
        public HttpResponseMessage GetEntriesToTOC([FromUri]int tocId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<EntryDTO> entryList = dao.getEntriesForToc(tocId);
            response = Request.CreateResponse(HttpStatusCode.OK, entryList);
            return response;
        }


        // GET: api/Entry/5
        public HttpResponseMessage Get(int ID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            EntryDTO entry = dao.getEntry(ID);
            EntryDTO entryDTO = null;
            response = Request.CreateResponse(HttpStatusCode.OK, entry);
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
