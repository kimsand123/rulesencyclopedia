using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class FullTOCDTO
    {
        internal TOCDTO toc { get; set; }
        internal List<EntryDTO> entryList {get;set;}
    }
}