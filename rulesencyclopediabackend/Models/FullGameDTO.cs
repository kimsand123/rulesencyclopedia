using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Models
{
    public class FullGameDTO
    {
        GameDTO game { get; set; }
        List<FullTOCDTO> tocList { get; set; }
    }
}