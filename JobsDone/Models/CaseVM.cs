using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Models
{
    public class CaseVM
    {
        public Case Case { get; set; }
        public List<TaskVM> Tasks { get; set; }
    }
}