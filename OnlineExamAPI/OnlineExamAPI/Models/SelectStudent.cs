using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamAPI.Models
{
   
        public class SelectStudent
        {
            public string Technology { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public int? Level { get; set; }
            public int? marks { get; set; }
        }
    
}