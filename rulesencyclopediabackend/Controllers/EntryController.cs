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
        EntryDTO entryDTO;
        ConvertToDTO DTOConverter = new ConvertToDTO();
        public HttpResponseMessage GetEntriesToTOC([FromUri]int tocId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
 

            List<Entry> entryList = dao.getEntriesForToc(tocId);
            List<EntryDTO> entryDTOs = new List<EntryDTO>();
            foreach (Entry entry in entryList)
            {
                entryDTO = (EntryDTO)DTOConverter.Converter(new EntryDTO(), entry);
                entryDTOs.Add(entryDTO);
            }

            try
            {
                string responseJson = JsonConvert.SerializeObject(entryDTOs);
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


        // GET: api/Entry/5
        public HttpResponseMessage Get(int ID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Entry entry = dao.getEntry(ID);
            entryDTO = (EntryDTO)DTOConverter.Converter(new EntryDTO(), entry);
            try
            {
                string responseJson = JsonConvert.SerializeObject(entryDTO);
            } catch (JsonSerializationException ex)
            {
                // TODO: write ex to logfile.
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the entry");
            }
            finally
            {
                // TODO: close the logfile
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
