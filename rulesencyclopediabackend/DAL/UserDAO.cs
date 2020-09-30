using rulesencyclopedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend
{
    public class UserDAO
    {
        List<UserDTO> userList = new List<UserDTO>();
        public UserDAO()
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
            UserDTO user2 = new UserDTO();
            user2.ID = 2;
            user2.FirstName = "Mette";
            user2.MiddleName = "";
            user2.LastName = "Nielsen";
            user2.UserName = "Aurafalaura";
            user2.Password = "2222";
            user2.Date = DateTime.Today;

            userList.Add(user2);
        }

        public List<rulesencyclopedia.UserDTO> getUserList()
        {
            return userList;
        }

        public UserDTO getUser(int id)
        {
            UserDTO user;
            int index = id - 1;
            user = userList.ElementAt(index);
            return user;
        }

        public int getNumberOfUsers()
        {
            return userList.Count;
        }

    }
}