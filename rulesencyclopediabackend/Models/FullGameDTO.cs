using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class FullGameDTO
    {
        internal GameDTO game { get; set; }
        internal List<FullTOCDTO> tocList { get; set; }
    }
}