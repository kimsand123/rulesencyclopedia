using System;

namespace rulesencyclopediabackend.Models
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
        public string Salt { get; set; }
    }
}