﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication10.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class testtestEntities4 : DbContext
    {
        public testtestEntities4()
            : base("name=testtestEntities4")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<feedback> feedback { get; set; }
        public virtual DbSet<hiringRequest> hiringRequest { get; set; }
        public virtual DbSet<notification> notification { get; set; }
        public virtual DbSet<progress> progress { get; set; }
        public virtual DbSet<projectdetails> projectdetails { get; set; }
        public virtual DbSet<projectRequest> projectRequest { get; set; }
        public virtual DbSet<projects> projects { get; set; }
        public virtual DbSet<projectteams> projectteams { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<users> users { get; set; }
    }
}
