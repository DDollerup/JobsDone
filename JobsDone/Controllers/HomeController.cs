using JobsDone.Factories;
using JobsDone.Helpers;
using JobsDone.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JobsDone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DBContext context = new DBContext();
        private User user;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            user = context.UserFactory.Get(Convert.ToInt32(User.Identity.Name));
            Session["User"] = user;
            base.OnActionExecuting(filterContext);
        }

        public static string RenderPartialToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult Index()
        {
            return View(context.CaseFactory.GetAllBy("UserID", user.ID));
        }

        public ActionResult ShowCase(int id = 0)
        {
            return View(VMFactory.CreateCaseVM(id));
        }

        public ActionResult AddCaseModal()
        {
            var result = new { html = RenderPartialToString(this, "Partials/AddCasePartial", null) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddCase(Case @case)
        {
            @case.UserID = user.ID;
            context.CaseFactory.Insert(@case);
            return RedirectToAction("Index");
        }
    }
}