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
    
    public partial class OnlineExamEntities13 : DbContext
    {
        public OnlineExamEntities13()
            : base("name=OnlineExamEntities13")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admins { get; set; }
        public DbSet<LevelTable> LevelTables { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ReportCard> ReportCards { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TestSubject> TestSubjects { get; set; }
    
        public virtual ObjectResult<fectchStudent_Result> fectchStudent()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fectchStudent_Result>("fectchStudent");
        }
    
        public virtual ObjectResult<fetchLevel_Result> fetchLevel()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fetchLevel_Result>("fetchLevel");
        }
    
        public virtual ObjectResult<fetchqusn_Result> fetchqusn()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fetchqusn_Result>("fetchqusn");
        }
    
        public virtual ObjectResult<fetchSubject_Result> fetchSubject()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fetchSubject_Result>("fetchSubject");
        }
    
        public virtual ObjectResult<myReport_Result> myReport(Nullable<int> sid)
        {
            var sidParameter = sid.HasValue ?
                new ObjectParameter("sid", sid) :
                new ObjectParameter("sid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<myReport_Result>("myReport", sidParameter);
        }
    
        public virtual int sp_UpdateLevel(Nullable<int> lid, Nullable<int> level)
        {
            var lidParameter = lid.HasValue ?
                new ObjectParameter("Lid", lid) :
                new ObjectParameter("Lid", typeof(int));
    
            var levelParameter = level.HasValue ?
                new ObjectParameter("Level", level) :
                new ObjectParameter("Level", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdateLevel", lidParameter, levelParameter);
        }
    
        public virtual int sp_UpdatePassword(string otp, string password)
        {
            var otpParameter = otp != null ?
                new ObjectParameter("otp", otp) :
                new ObjectParameter("otp", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdatePassword", otpParameter, passwordParameter);
        }
    
        public virtual ObjectResult<MyLevel_Result> MyLevel(Nullable<int> sid)
        {
            var sidParameter = sid.HasValue ?
                new ObjectParameter("sid", sid) :
                new ObjectParameter("sid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MyLevel_Result>("MyLevel", sidParameter);
        }
    
        public virtual ObjectResult<allevel_Result> allevel()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<allevel_Result>("allevel");
        }
    
        public virtual ObjectResult<allreport_Result> allreport()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<allreport_Result>("allreport");
        }
    
        public virtual ObjectResult<unique_file_sp_Result> unique_file_sp()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<unique_file_sp_Result>("unique_file_sp");
        }
    
        public virtual int sp_UpdateSubject(Nullable<int> subjectId, string subject, Nullable<int> totalMark, Nullable<int> passingMark, Nullable<int> examDuration, string tStatus)
        {
            var subjectIdParameter = subjectId.HasValue ?
                new ObjectParameter("SubjectId", subjectId) :
                new ObjectParameter("SubjectId", typeof(int));
    
            var subjectParameter = subject != null ?
                new ObjectParameter("Subject", subject) :
                new ObjectParameter("Subject", typeof(string));
    
            var totalMarkParameter = totalMark.HasValue ?
                new ObjectParameter("TotalMark", totalMark) :
                new ObjectParameter("TotalMark", typeof(int));
    
            var passingMarkParameter = passingMark.HasValue ?
                new ObjectParameter("PassingMark", passingMark) :
                new ObjectParameter("PassingMark", typeof(int));
    
            var examDurationParameter = examDuration.HasValue ?
                new ObjectParameter("ExamDuration", examDuration) :
                new ObjectParameter("ExamDuration", typeof(int));
    
            var tStatusParameter = tStatus != null ?
                new ObjectParameter("TStatus", tStatus) :
                new ObjectParameter("TStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdateSubject", subjectIdParameter, subjectParameter, totalMarkParameter, passingMarkParameter, examDurationParameter, tStatusParameter);
        }
    }
}
