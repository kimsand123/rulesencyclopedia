using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using System.Text.Json;
using System.Text.Json.Serialization;
using rulesencyclopediabackend.DAL;
using rulesencyclopediabackend.Exceptions;

namespace rulesencyclopediabackend.Controllers
{
    public class UserController : ApiController
    {
        UserDAO dao = new UserDAO();
        ExceptionHandling exHandler = new ExceptionHandling();
        // GET api/users
        public string Get()
        {
            List<User> userList = dao.getUserList();
            string responseJson = JsonSerializer.Serialize(userList);
            return responseJson;
        }

         // GET api/users/5
        public String Get(int id)
        {
            User user = dao.getUser(id);
            string responseJson = JsonSerializer.Serialize(user);
            return responseJson;
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
