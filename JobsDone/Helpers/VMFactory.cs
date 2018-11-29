using JobsDone.Factories;
using JobsDone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Helpers
{
    public static class VMFactory
    {
        private static DBContext context = new DBContext();

        public static CaseVM CreateCaseVM(int caseID)
        {
            CaseVM vm = new CaseVM()
            {
                Case = context.CaseFactory.Get(caseID),
                Tasks = CreateTaskVMs(caseID),
            };
            vm.User = context.UserFactory.Get(vm.Case.UserID);
            return vm;
        }

        public static List<TaskVM> CreateTaskVMs(int caseID)
        {
            List<TaskVM> taskVMs = new List<TaskVM>();
            foreach (Relation item in context.RelationFactory.GetAllBy("CaseID", caseID))
            {
                TaskVM taskVM = new TaskVM()
                {
                    Task = context.TaskFactory.Get(item.TaskID),
                    Relation = item
                };
                taskVMs.Add(taskVM);
            }
            return taskVMs;
        }

        public static List<CaseVM> GetCaseVMsByUserID(int userID)
        {
            List<CaseVM> cases = new List<CaseVM>();
            foreach (Case item in context.CaseFactory.GetAllBy("UserID", userID))
            {
                CaseVM vm = CreateCaseVM(item.ID);
                cases.Add(vm);
            }
            return cases;
        }
    }
}