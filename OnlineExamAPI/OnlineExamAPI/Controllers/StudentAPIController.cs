using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using OnlineExamAPI.Models;
using System.Web.Http.Cors;

namespace OnlineExamAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    [Route("api/StudentAPI")]
    public class StudentAPIController : ApiController
    {
        OnlineExamEntities db = new OnlineExamEntities();
        [Route("api/StudentAPI/RegisterStudent")]
        [HttpPost]
        public bool Post([FromBody] Student stud)
        {
            try
            {
                db.Students.Add(stud);
                var res = db.SaveChanges();
                if (res > 0)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        [Route("api/StudentAPI/Login/{email}/{pwd}")]
        [HttpGet]
        public string Get(string email, string pwd)
        {
            string result = "";
            try
            {
                var data = db.Students.Where(x => x.Email == email && x.Password == pwd);
                if (data.Count() == 0)
                    result = "Invalid credentials";
                else
                    result = "Login successful";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/StudentAPI/ShowAllStudents")]
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            try
            {
                var data = db.Students.ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
