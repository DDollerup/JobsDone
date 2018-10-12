using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Models
{
    public class Case
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }
    }
}