﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class FullGameDTO
    {
        public GameDTO game { get; set; }
        public List<FullTOCDTO> tocList { get; set; }
    }
}