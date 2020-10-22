﻿using Newtonsoft.Json;
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
        ConvertToDTO DTOConverter = new ConvertToDTO();
        TOCDTO tocDTO;
        // GET: api/TOC
        public HttpResponseMessage GetTocsForGame([FromBody] int gameId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<TOCDTO> tocList = dao.getTOCList(gameId);
            try
            {
                string responseJson = JsonConvert.SerializeObject(tocList);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);

            }
            catch (JsonSerializationException ex)
            {
                // TODO: write ex to a logfile
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the TOC list");
            }
            finally
            {
                // TODO: close the logfile
            }
            return response;
            
        }

        // GET: api/TOC/5
        public HttpResponseMessage Get(int ID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            TOCDTO toc = dao.getTOC(ID);
            try
            {
                string responseJson = JsonConvert.SerializeObject(toc);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);
            }catch (JsonSerializationException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Serverproblems Problems with serializing the TOC");
            }
            finally
            {
                // TODO: close the logfile
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

