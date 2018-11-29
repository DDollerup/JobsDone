using JobsDone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Factories
{
    public class DBContext
    {
        private AutoFactory<Case> caseFactory;
        private AutoFactory<Company> companyFactory;
        private AutoFactory<Relation> relationFactory;
        private AutoFactory<Task> taskFactory;
        private UserFactory userFactory;
        private AutoFactory<Role> roleFactory;

        public AutoFactory<Case> CaseFactory
        {
            get
            {
                return caseFactory != null ? caseFactory : new AutoFactory<Case>();
            }
        }

        public AutoFactory<Company> CompanyFactory
        {
            get
            {
                return companyFactory != null ? companyFactory : new AutoFactory<Company>();
            }
        }

        public AutoFactory<Relation> RelationFactory
        {
            get
            {
                return relationFactory != null ? relationFactory : new AutoFactory<Relation>();
            }
        }

        public AutoFactory<Task> TaskFactory
        {
            get
            {
                return taskFactory != null ? taskFactory : new AutoFactory<Task>();
            }
        }

        public UserFactory UserFactory
        {
            get
            {
                return userFactory != null ? userFactory : new UserFactory();
            }
        }

        public AutoFactory<Role> RoleFactory
        {
            get
            {
                return roleFactory != null ? roleFactory : new AutoFactory<Role>();
            }
        }
    }
}