using MySql.Data.MySqlClient;
using rulesencyclopediabackend;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace rulesencyclopedia
{
    public class UserDTO
    {

        public UserDTO()
        {

        }

        public UserDTO(User user)
        {
            this.ID = user.Id;
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.Password = user.Password;
            this.Date = (DateTime)user.Date;
        }
        public int ID
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;

        }

        public DateTime Date
        {
            get;
            set;

        }
    }
}