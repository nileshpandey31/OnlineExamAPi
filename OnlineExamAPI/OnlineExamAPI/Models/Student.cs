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
    
    public partial class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string College { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Qualifacton { get; set; }
        public string YearOfCompletion { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<bool> EmailVerification { get; set; }
        public string OTP { get; set; }
        public Nullable<System.Guid> ActivetionCode { get; set; }
    }
}
