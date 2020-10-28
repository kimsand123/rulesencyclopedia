using rulesencyclopediabackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    [Serializable]
    public class TokenDTO
    {
        public string token { get; set; }
        public DateTime timestamp { get; set; }
    }
}