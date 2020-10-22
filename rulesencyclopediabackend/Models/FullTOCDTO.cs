using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class FullTOCDTO
    {
        TOCDTO toc { get; set; }
        List<EntryDTO> entryList {get;set;}
    }
}