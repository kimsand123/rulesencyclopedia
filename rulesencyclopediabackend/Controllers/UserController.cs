using Microsoft.Ajax.Utilities;
using rulesencyclopedia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rulesencyclopediabackend.Controllers
{
    public class UserController : ApiController
    {
        UserDTO[] user = new UserDTO[10];
        UserDTO[0] = new UserDTO{1,"Kim","Sandberg","Bossen","Kimsand",02121970};
        // GET api/users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/users/5
        public UserDTO Get(int id)
        {
            if (id < 10)
            {
                if(user[id]!=null)
                {
                    return user[id];
                }
            }
            return null;
            
        }

        // POST api/users
        public void Post([FromBody] UserDTO user)
        {
            if (this.user.Length < 11)
            {
                int numberOfUsers = this.user.Length;

                UserDTO newUser = new UserDTO();
                newUser.ID = this.user.Length;
                newUser.FirstName = user.FirstName;
                newUser.MiddleName = user.MiddleName;
                newUser.LastName = user.LastName;
                newUser.UserName = user.UserName;
                newUser.Password = user.Password;
                newUser.Date = user.Date;
                this.user[newUser.ID] = newUser;
            }
            else
            {
             
            }

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
