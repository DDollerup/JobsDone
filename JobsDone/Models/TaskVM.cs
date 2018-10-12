using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Models
{
    public class TaskVM
    {
        public Task Task { get; set; }
        public Relation Relation { get; set; }
    }
}