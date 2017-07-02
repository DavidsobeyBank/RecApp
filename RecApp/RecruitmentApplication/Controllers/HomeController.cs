using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecruitmentApplication.Models;
using System.Web.UI.WebControls;
using RecruitmentApplication.ViewModels;
using System.Text.RegularExpressions;
using System.Collections;
using SimpleCrypto;

namespace RecruitmentApplication.Controllers
{
    public class HomeController : Controller
    {
        private RecruitmentAppEntities db = new RecruitmentAppEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region Login Stuff

        [HttpPost]
        public ActionResult Index(string eMail, string pass)
        {
            string email = Request["Email"].ToString();

            //hardcoded
            string password = "guest";

            //check if user exists and password matches
            var user = db.Employees.FirstOrDefault(e => e.EmployeeEmail == email);
            if(user != null)
            {
                ICryptoService cryptoService = new PBKDF2();

                string hashed = cryptoService.Compute(password, user.Salt);

                if (hashed == user.Pass)
                {
                    Session["userLoggedIn"] = user.EmployeeEmail;
                    return RedirectToAction("Index", "Interviews");

                }
            }
            
            return View();
        }
        #endregion

        public ActionResult Logout()
        {
            Session["userLoggedIn"] = null;
            return Redirect("~/Home/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}