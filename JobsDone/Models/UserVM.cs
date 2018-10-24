using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Models
{
    public class UserVM
    {
        public User User { get; set; }
        public Company Company { get; set; }
        public List<CaseVM> Cases { get; set; }

        public int TasksCount
        {
            get
            {
                int taskCount = 0;
                foreach (CaseVM item in Cases) taskCount += item.Tasks.Count;
                return taskCount;
            }
        }

        public int CasesFinished
        {
            get
            {
                int casesFinished = 0;
                foreach (CaseVM item in Cases) casesFinished += item.Case.EndDate != DateTime.MinValue ? 1 : 0;
                return casesFinished;
            }
        }

        public int CasesOpen
        {
            get
            {
                int casesOpen = 0;
                foreach (CaseVM item in Cases) casesOpen += item.Case.EndDate == DateTime.MinValue ? 1 : 0;
                return casesOpen;
            }
        }
    }
}