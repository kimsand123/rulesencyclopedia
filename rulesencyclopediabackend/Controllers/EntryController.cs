using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class EntryController : ApiController
    {
        // GET: api/Entry/5
        public string Get(int TOCId)
        {
            string result = "";
            List<Entry> rules = EntryDAO.getEntriesForToc(TOCId);
            
            return result;
        }

        // POST: api/Entry
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Entry/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Entry/5
        public void Delete(int id)
        {
        }
    }
}
