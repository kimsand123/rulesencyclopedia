using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class TOCController : ApiController
    {
        // GET: api/TOC
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TOC/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TOC
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TOC/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TOC/5
        public void Delete(int id)
        {
        }
    }
}
