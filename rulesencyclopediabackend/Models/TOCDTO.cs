using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class TOCDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Revision { get; set; }
        public string Editor { get; set; }
    }
}