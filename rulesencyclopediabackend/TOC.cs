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
    
    public partial class TOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOC()
        {
            this.Entry = new HashSet<Entry>();
        }
    
        public int Id { get; set; }
        public string Text { get; set; }
        public int Revisions { get; set; }
        public string Editor { get; set; }
        public int Game { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entry> Entry { get; set; }
        public virtual Game Game1 { get; set; }
    }
}
