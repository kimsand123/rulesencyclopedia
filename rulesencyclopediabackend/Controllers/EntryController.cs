using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Configuration;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using rulesencyclopediabackend.Auth;

namespace rulesencyclopediabackend.Controllers
{   
    public class EntryController : ApiController
    {
        EntryDAO dao = new EntryDAO();
        ConvertToDTO DTOConverter = new ConvertToDTO();

        [BasicAuthentication]
        public HttpResponseMessage GetEntriesToTOC([FromUri]int tocId)
        {
            List<EntryDTO> entryList = dao.getEntriesForToc(tocId);
            if (entryList != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entryList);
            } else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        // GET: api/Entry/5
        [BasicAuthentication]
        public HttpResponseMessage Get(int ID)
        {
            EntryDTO entry = dao.getEntry(ID);
            if (entry != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST: api/Entry
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]Entry entry)
        {
            //Posting the Entry, and getting its ID
            //-999999 means that the dao had an error.
            var result = dao.postEntry(entry);
            if (result != -999999)
            {
                //returning http code Created and the link to the ressource according to REST.
                return Request.CreateResponse(HttpStatusCode.Created, "/api/Entry/" + result);
            } else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error when creating entry");
            }

        }

        // PUT: api/Entry/5
        [BasicAuthentication]
        public HttpResponseMessage Put([FromBody]EntryDTO entry)
        {
            var result = dao.editEntry(entry);
            if (result != -999999)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "/api/Entry/" + entry.Id);
            } else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error when editing entry");
            }
        }

        // DELETE: api/Entry/5
        [BasicAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            var result = dao.deleteEntry(id);
            if (result != -999999)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Entry is deleted");
            } else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error when deleting entry");
            }
        }
    }
}
