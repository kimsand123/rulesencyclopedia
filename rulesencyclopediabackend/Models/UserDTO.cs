using MySql.Data.MySqlClient;
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

        public UserDTO(List<String> result)
        {
            this.ID = int.Parse(result[0].ToString());
            this.FirstName = result[1].ToString();
            this.MiddleName = result[2].ToString();
            this.LastName = result[3].ToString();
            this.UserName = result[4].ToString();
            this.Password = result[5].ToString();
            this.Date = Convert.ToDateTime(result[6].ToString());
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