﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineExamAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class OnlineExamEntities2 : DbContext
    {
        public OnlineExamEntities2()
            : base("name=OnlineExamEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TestSubject> TestSubjects { get; set; }
    
        public virtual ObjectResult<fetchqusn_Result> fetchqusn()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fetchqusn_Result>("fetchqusn");
        }
    }
}
