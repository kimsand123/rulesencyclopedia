using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using rulesencyclopediabackend.Auth;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;

namespace rulesencyclopediabackend.Controllers
{
    public class UserController : ApiController
    { 
        UserDAO dao = new UserDAO();

        // GET api/User
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<UserDTO> userList = dao.getUserList();
            if (userList != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, userList);
            } else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Userlist not found");
            }
            return response;
        }

        // GET api/User/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            UserDTO user = dao.getUser(id);
            if (user != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            } 
            return response;
        }

        // POST api/User
        public HttpResponseMessage Post([FromBody] User user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            int id = dao.postUser(user);
            if (id != -999999)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "/api/User/" + id);
            } else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error when creating user " + user.UserName);
            }
        }

        // PUT api/User/5
        [BasicAuthentication]
        public HttpResponseMessage Put([FromBody] User user)
        {
            var result = dao.editUser(user);
            if (result != -999999)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User " + user.UserName + " has been edited");
            } else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error when editing user " + user.UserName);
            }
        }

        // DELETE api/User/5
        [BasicAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            var result = dao.deleteUser(id);
            if (result != -999999)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User is deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error when deleting user");
            }
        }
    }
}
