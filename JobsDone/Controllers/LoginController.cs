using JobsDone.Factories;
using JobsDone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsDone.Controllers
{
    public class LoginController : Controller
    {
        private DBContext context = new DBContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            var userToLogin = context.UserFactory.Login(user.Email, user.Password);
            if (userToLogin.ID > 0)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                ViewBag.LoginError = "Wrong username or password.";
                return View(user);
            }
        }
    }
}