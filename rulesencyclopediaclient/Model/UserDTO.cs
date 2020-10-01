using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rulesencyclopediaclient.Model
{
    class UserDTO
    {
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
