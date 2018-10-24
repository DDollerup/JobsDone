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
            if (TempData["SearchResult"] != null)
            {
                return View((TempData["SearchResult"] as List<Case>).OrderByDescending(x => x.StartDate).ToList());
            }
            else
            {
                return View(context.CaseFactory.GetAllBy("UserID", user.ID).OrderByDescending(x => x.StartDate).ToList());
            }
        }

        public ActionResult ShowCase(int id = 0)
        {
            return View(VMFactory.CreateCaseVM(id));
        }

        #region Cases
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

        [HttpPost]
        public ActionResult EditCase(Case @case)
        {
            context.CaseFactory.Update(@case);
            var json = new { result = true };
            return Json(json);
        }

        [HttpPost]
        public ActionResult CaseSearch(string searchInput)
        {
            TempData["SearchResult"] = context.CaseFactory.SearchBy(searchInput, "Title", "Title", "StartDate", "EndDate", "ID").Where(x => x.UserID == user.ID).ToList();
            return RedirectToAction("Index");
        }
        #endregion

        #region Tasks
        public ActionResult AddTaskModal(int id = 0)
        {
            ViewBag.CaseID = id;
            var result = new { html = RenderPartialToString(this, "Partials/AddTaskPartial", context.TaskFactory.GetAllBy("CompanyID", user.CompanyID).OrderBy(x => x.Title).ToList()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddTask(int caseID, int taskID, string taskDescription, string taskPrice, string newTaskOption = null)
        {
            decimal.TryParse(taskPrice.Replace(".", ","), out decimal price);

            if (taskID == 0) taskID = context.TaskFactory.Insert(new Task() { Title = newTaskOption, CompanyID = user.CompanyID }).ID;

            Relation relation = new Relation()
            {
                CaseID = caseID,
                TaskID = taskID,
                Description = taskDescription,
                Price = price
            };
            context.RelationFactory.Insert(relation);
            return RedirectToAction("ShowCase", new { id = caseID });
        }

        [HttpPost]
        public ActionResult EditTask(int relationID, string relDescription, string relPrice, int taskID)
        {
            Relation relation = context.RelationFactory.Get(relationID);
            relation.TaskID = taskID;
            relation.Description = relDescription;
            relation.Price = decimal.Parse(relPrice.Replace(".", ","));
            context.RelationFactory.Update(relation);
            var json = new { result = Request.UrlReferrer.PathAndQuery };
            return Json(json);
        }

        // id is relation id
        public ActionResult DeleteTask(int id = 0)
        {
            context.RelationFactory.Delete(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpGet]
        public ActionResult GetTasks()
        {
            return Json(context.TaskFactory.GetAllBy("CompanyID", user.CompanyID).OrderBy(x => x.Title).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Profile
        public ActionResult Profile()
        {
            UserVM userVM = new UserVM();

            userVM.User = user;
            userVM.Company = context.CompanyFactory.Get(user.CompanyID);
            userVM.Cases = VMFactory.GetCaseVMsByUserID(user.ID);

            return View(userVM);
        }
        #endregion

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}