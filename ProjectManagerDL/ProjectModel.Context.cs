﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectManagerDL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectTasksDBEntities : DbContext
    {
        public ProjectTasksDBEntities()
            : base("name=ProjectTasksDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TaskDetail> TaskDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
