using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class GameDTO
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public int Revision { get; set; }
        public string Editor { get; set; }
    }
}