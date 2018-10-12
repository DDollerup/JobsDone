using JobsDone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace JobsDone.Factories
{
    public class UserFactory : AutoFactory<User>
    {
        public User Login(string email, string password)
        {
            string hash = GenerateSHA512Hash(password);
            User user = SqlQuery($"SELECT * FROM [User] WHERE Email='{email}' AND Password='{hash}'");
            if (user.ID > 0)
            {
                FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);
            }
            return user;
        }
    }
}