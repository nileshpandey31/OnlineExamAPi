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
    
    public partial class fetchSubject_Result
    {
        public int SubjectId { get; set; }
        public string Subject { get; set; }
        public Nullable<int> TotalMark { get; set; }
        public Nullable<int> PassingMark { get; set; }
        public Nullable<int> ExamDuration { get; set; }
        public string TStatus { get; set; }
    }
}
