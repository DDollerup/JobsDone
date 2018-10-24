using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CompanyID { get; set; }
    }
}