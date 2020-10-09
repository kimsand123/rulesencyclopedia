using Microsoft.Ajax.Utilities;
using rulesencyclopedia;
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

namespace rulesencyclopediabackend.Controllers
{
    public class UserController : ApiController
    {
        UserDAO userDao = new UserDAO();

        // GET api/users
        public String Get()
        {
            List<User> userList = userDao.getUserList();
            string responseJson = JsonSerializer.Serialize(userList);
            return responseJson;
        }

         // GET api/users/5
        public String Get(int id)
        {
            User user = userDao.getUser(id);
            string responseJson = JsonSerializer.Serialize(user);
            return responseJson;
        }

        // POST api/users
        public void Post([FromBody] User user)
        {
            userDao.postUser(user);
        }

        // PUT api/users/5
        public void Put(int id, [FromBody] User user)
        {
            userDao.editUser(id, user);
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
            userDao.deleteUser(id);
        }
    }
}
