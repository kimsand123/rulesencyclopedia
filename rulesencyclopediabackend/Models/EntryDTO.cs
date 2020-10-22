using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class EntryDTO
    {
        public string Headline { get; set; }
        public string ParagraphNumber {get;set;}
        public string Text { get; set; }
        public int Type { get; set; }
        public int Revision { get; set; }
        public string Editor { get; set; }
    }
}