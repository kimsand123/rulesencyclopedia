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
using System.Runtime.Serialization;

namespace rulesencyclopediabackend.Controllers
{
    public class UserController : ApiController
    {
        List<UserDTO> userList = new List<UserDTO>();
        
        UserController()
        {
            UserDTO user = new UserDTO();
            user.ID = 1;
            user.FirstName = "Kim";
            user.MiddleName = "Sandberg";
            user.LastName = "Bossen";
            user.UserName = "kimsand";
            user.Password = "1234";
            user.Date = DateTime.Today;

            userList.Add(user);

            user.ID = 2;
            user.FirstName = "Mete";
            user.MiddleName = "";
            user.LastName = "Nielsen";
            user.UserName = "Aurafalaura";
            user.Password = "2222";
            user.Date = DateTime.Today;

            userList.Add(user);
        }
        

        // GET api/users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/users/5
        public UserDTO Get(int id)
        {
            UserDTO tmpUser = new UserDTO();
            if (id < userList.Count)
            {
                tmpUser = userList.ElementAt(id);
                return tmpUser;
            }
            return tmpUser;
            
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
