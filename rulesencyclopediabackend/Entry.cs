//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rulesencyclopediabackend
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entry
    {
        public int Id { get; set; }
        public string ParagraphNumber { get; set; }
        public string Revision { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public string Editor { get; set; }
        public string Headline { get; set; }
        public int TOC { get; set; }
    
        public virtual TOC TOC1 { get; set; }
    }
}
