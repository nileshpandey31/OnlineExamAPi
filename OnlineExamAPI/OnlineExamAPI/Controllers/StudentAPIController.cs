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
using System.Net.Mail;
using System.Runtime;

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
        OnlineExamEntities2 db = new OnlineExamEntities2();   //db instance

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

        [Route("api/StudentAPI/ShowQst")]
        [HttpGet]

        public IEnumerable<fetchqusn_Result> ShowQt()      // will return list of Quesn object
        {
            try
            {
                var Qsn = db.fetchqusn();
                return Qsn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/StudentAPI/CheckEmail/{email}")]
        [HttpGet]
        public string Get(string email)
        {
            string result = "";
            try
            {
                var data = db.Students.Where(x => x.Email == email).SingleOrDefault();
                if (data != null)
                {
                    result = "success";
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        [Route("api/StudentAPI/VerifyLinkEmail")]
        [HttpPost]
        public string post([FromBody] Student stud)
        {
            string result = "";
            try
            {
                var data = db.Students.Where(x => x.Email == stud.Email).FirstOrDefault();
                if (data == null)
                    return result;
                string OTP = GeneratePassword();
                data.ActivetionCode = Guid.NewGuid();
                data.OTP = OTP;
                db.Entry(data).State = System.Data.EntityState.Modified;
                var res = db.SaveChanges();
                if (res > 0)
                {
                    ForgetPasswordEmailToUser(data.Email, data.ActivetionCode.ToString(), data.OTP);
                    result = "success";
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GeneratePassword()                //Generates OTP for ForgotPassword
        {
            string OTPLength = "4";
            string OTP = string.Empty;

            string Chars = string.Empty;
            Chars = "1,2,3,4,5,6,7,8,9,0";

            char[] seplitChar = { ',' };
            string[] arr = Chars.Split(seplitChar);
            string NewOTP = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NewOTP += temp;
                OTP = NewOTP;
            }
            return OTP;
        }

        public void ForgetPasswordEmailToUser(string emailid, string activationCode, string OTP)
        {

            var GenarateUserVerificationLink = "//localhost:4200/PassReset/";

            /*
            var GenarateUserVerificationLink= "//localhost:4200/PassRest/" + activationCode;
            string current_url = System.Web.HttpContext.Current.Request.Url.ToString();
            var link = System.Web.HttpContext.Current.Request.Url.ToString().Replace(current_url,GenarateUserVerificationLink);*/

            var link = GenarateUserVerificationLink;

            var fromMail = new MailAddress("test962326@gmail.com", "testing"); //enter your mail id
            var fromEmailpassword = "9921214432"; // Set your email password
            var toEmail = new MailAddress(emailid);

            var smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Password Reset-Demo";
            Message.Body = "< br /> Please click on the below link for password change " + "<br/><a href=" + link + ">" + link + "</a>" +
              "<br/> OTP for password change : " + OTP;
            Message.IsBodyHtml = true;

            smtp.Send(Message);
        }

        [Route("api/StudentAPI/SetNewPassword")]
        [HttpPost]
        public bool Put([FromBody] Student stud)
        {
            bool result = false;
            try
            {
                string otp = stud.OTP; ;
                string NewPass = stud.Password;

                var res = db.sp_UpdatePassword(otp, NewPass);
                if (res > 0)
                {
                    result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


    }
}
