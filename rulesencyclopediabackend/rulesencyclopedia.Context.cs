﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class rulesencyclopediaDBEntities1 : DbContext
    {
        public rulesencyclopediaDBEntities1()
            : base("name=rulesencyclopediaDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<TOC> TOC { get; set; }
    }
}
