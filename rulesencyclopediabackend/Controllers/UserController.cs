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
            response = Request.CreateResponse(HttpStatusCode.OK, userList);
            return response;
        }

         // GET api/users/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            User user = dao.getUser(id);
            response = Request.CreateResponse(HttpStatusCode.OK, user);
            return response;
        }

        // Get api/users/username
        public HttpResponseMessage Get(string userName)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            User user = dao.checkUserName(userName);
            if (user != null)
            {
                response = Request.CreateResponse(HttpStatusCode.Found, "Username "+userName+" is already in use");
            } else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Username is not in use ");
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
