using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExcelDataReader; //need to install first
using System.IO;
using OnlineExamAPI.Models;  // for database
using System.Web.Http.Cors; //for enabling cors
using System.Text;

namespace OnlineExamAPI.Controllers
{
    static class GlobalText    // global variable jugaad
    {
        public static int sid;
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")] // all permision given

    [Route("api/StudentAPI")]   // parent route
    
    public class StudentAPIController : ApiController
    {
        OnlineExamEntities1 db = new OnlineExamEntities1();   //db instance

        //method to insert student into student table with passwrd encryption

        [Route("api/StudentAPI/RegisterStudent")]
        [HttpPost]
        public bool Post([FromBody] Student stud)
        {
            try
            {
                // Console.WriteLine(stud.Password);

                stud.Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(stud.Password)));
                var result = db.Students.Add(stud);
                // Console.WriteLine(stud.Password);
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


        //method for login validation

        [Route("api/StudentAPI/Login/{email}/{pwd}")]
        [HttpGet]
        public string Get(string email, string pwd)
        {
            string result = "";
            try
            {
                pwd = Convert.ToBase64String
                    (System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pwd)));
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

        //method to display or etch all student

        [Route("api/StudentAPI/ShowAllStudents")]
        [HttpGet]                                                             // get method for displaying
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

        //method to insert qusn from excel to db

        [Route("api/StudentAPI/UploadExcel")]
        [HttpPost]
        public string ExcelUpload()
        {
            string message = "";
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
           

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;

                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        message = "This file format is not supported";
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var finalRecords = excelRecords.Tables[0];
                   
                    for (int i = 1; i < finalRecords.Rows.Count; i++)
                    {
                        Question objUser = new Question();
                        objUser.Qsn = finalRecords.Rows[i][0].ToString();
                        objUser.Opt1 = finalRecords.Rows[i][1].ToString();
                        objUser.Opt2 = finalRecords.Rows[i][2].ToString();
                        objUser.Opt3 = finalRecords.Rows[i][3].ToString();
                        objUser.Opt4 = finalRecords.Rows[i][4].ToString();
                        objUser.Answer = finalRecords.Rows[i][5].ToString();
                        objUser.Level= finalRecords.Rows[i][6].ToString();
                    objUser.FileName = file.FileName;           // fetch the name of file uploaded with extenson
                        objUser.SubjectId =GlobalText.sid;
                   
                    db.Questions.Add(objUser);

                    }


                    int output = db.SaveChanges();
                    if (output > 0)
                    {
                        message = file.FileName + " Excel file has been successfully uploaded";
                      

                }
                    else
                    {
                        message = "Excel file uploaded has fiald";
                    }

                }

                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
           
            return message;
        }

        //method to insert subject into subject table

        [Route("api/StudentAPI/TestSubject")]
        [HttpPost]
        public bool Post([FromBody] TestSubject ts)       
        {
            try
            {
                db.TestSubjects.Add(ts);
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

        //method to add subjectid in the quesn table in frnt of respective filename

        [Route("api/StudentAPI/AddSubId")]
        [HttpPost]
        public bool AddSubID([FromBody]  int Sid )
        {

            try
            {
                GlobalText.sid = Sid;
               
                return true;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        //method to show or fetch  name of subject

        [Route("api/StudentAPI/ShowSubject")]
        [HttpGet]

        public IEnumerable<TestSubject> Subject()      // will return list of subject object
        {
            try
            {
                var data = db.TestSubjects.ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
