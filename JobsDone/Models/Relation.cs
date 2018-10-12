using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Models
{
    public class Relation
    {
        public int ID { get; set; }
        public int CaseID { get; set; }
        public int TaskID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}