using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace rulesencyclopediabackend.Controllers
{
    public class UserController : ApiController
    { 
        UserDAO dao = new UserDAO();
        // GET api/users
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<User> userList = dao.getUserList();
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string responseJson = JsonConvert.SerializeObject(userList, Formatting.Indented, serializerSettings);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);
            } catch (JsonSerializationException ex)
            {
                // TODO: write ex to logfile.
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Cannot serialize the userList");
            } finally
            {
                // TODO: close the logfile
            }
            return response;
        }

         // GET api/users/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            User user = dao.getUser(id);
            try
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string responseJson = JsonConvert.SerializeObject(user, Formatting.Indented, serializerSettings);
                response = Request.CreateResponse(HttpStatusCode.OK, responseJson);
            }
            catch (JsonSerializationException ex)
            {
                // TODO: write ex to logfile.
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Cannot serialize the user");
            }
            finally
            {
                // TODO: close the logfile
            }
            return response;
        }

        // POST api/users
        public void Post([FromBody] User user)
        {
            dao.postUser(user);
        }

        // PUT api/users/5
        public void Put(int id, [FromBody] User user)
        {
            dao.editUser(id, user);
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
            dao.deleteUser(id);
        }
    }
}
