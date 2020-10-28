﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    [Serializable]
    public class FullTOCDTO
    {
        public TOCDTO toc { get; set; }
        public List<EntryDTO> entryList {get;set;}
    }
}