//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ReportCard
    {
        public int ReportId { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public Nullable<int> Marks { get; set; }
        public Nullable<int> SLevel { get; set; }
        public string RStatus { get; set; }
        public Nullable<System.DateTime> ExamDate { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual TestSubject TestSubject { get; set; }
    }
}
