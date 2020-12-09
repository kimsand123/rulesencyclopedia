using Newtonsoft.Json;
using rulesencyclopediabackend.Auth;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class TOCController : ApiController
    {
        TOCDAO dao = new TOCDAO();
   
        // GET: api/TOC
        [BasicAuthentication]
        public HttpResponseMessage GetTocsForGame([FromUri] int gameId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<TOCDTO> tocList = dao.getTOCList(gameId);
            if (tocList.Count != 0)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, tocList);
            } else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "No TOCs found");
            }
            return response;
            
        }

        [BasicAuthentication]
        public HttpResponseMessage Get(int Id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            TOCDTO toc = dao.getTOC(Id);
            if (toc != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, toc);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Toc not found");
            }
            return response;
        }


        //Not in use, not implemented fully
        // POST: api/TOC
        [BasicAuthentication]
        public void Post([FromBody] TOC toc)
        {
            int tocId = dao.postTOC(toc);
        }

        //Not in use, not implemented fully
        // PUT: api/TOC/5
        [BasicAuthentication]
        public void Put([FromBody] TOC toc)
        {
            int tocId = dao.editTOC(toc);
        }

        //Not in use, not implemented fully
        // DELETE: api/TOC/5
        [BasicAuthentication]
        public void Delete(int id)
        {
            int tocId = dao.deleteTOC(id);
        }
    }
}

