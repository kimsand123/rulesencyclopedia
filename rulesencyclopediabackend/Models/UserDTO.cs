using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace rulesencyclopedia
{
    public class UserDTO
    {

        public UserDTO()
        {

        }

        public UserDTO(MySqlDataReader reader)
        {
            this.ID = int.Parse(reader[0].ToString());
            this.FirstName = reader[1].ToString();
            this.MiddleName = reader[2].ToString();
            this.LastName = reader[3].ToString();
            this.UserName = reader[4].ToString();
            this.Password = reader[5].ToString();
            this.Date = Convert.ToDateTime(reader[6].ToString());
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