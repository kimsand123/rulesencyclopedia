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
        List<UserDTO> userList = new List<UserDTO>();      

        // GET api/users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/users/5
        public String Get(int id)
        {
            UserDTO tmpUser = new UserDTO();
            if (id <= userDao.getNumberOfUsers())
            {
                tmpUser = userDao.getUser(id);
                string responseJson = JsonSerializer.Serialize(tmpUser);
                return responseJson;
            }
            return "nothing";
            
        }

        // POST api/users
        public void Post([FromBody] UserDTO user)
        {
            userList.Add(user);
        }

        // PUT api/users/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
        }
    }
}
