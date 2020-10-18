﻿using Newtonsoft.Json;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class TOCController : ApiController
    {
        ExceptionHandling exHandler = new ExceptionHandling();
        TOCDAO dao = new TOCDAO();
        // GET: api/TOC
        public HttpResponseMessage Get([FromBody] GameDTOFromView gameData)
        {
            
            HttpResponseMessage response = new HttpResponseMessage();
            List<TOC> tocList = dao.getTOCList(gameData.gameID);
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string responseJson = JsonConvert.SerializeObject(tocList, Formatting.Indented, serializerSettings);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);

            }
            catch (JsonSerializationException ex)
            {
                // TODO: write ex to a logfile
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the TOC list");
            }
            return response;
            
        }

        // GET: api/TOC/5
        public HttpResponseMessage Get(int ID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            TOC toc = dao.getTOC(ID);
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string responseJson = JsonConvert.SerializeObject(toc, Formatting.Indented, serializerSettings);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);
            }catch (JsonSerializationException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the TOC");
            }
            return response;
        }
            
        

        // POST: api/TOC
        public void Post([FromBody] TOC toc)
        {
            dao.postTOC(toc);
        }

        // PUT: api/TOC/5
        public void Put(int id, [FromBody] TOC toc)
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

