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
            if(Request["Email"] == null || Request["Password"] == null)
            {
                return View();
            }
            string email = Request["Email"].ToString();
            string password = Request["Password"].ToString();

            //check if user exists and password matches
            var user = db.Employees.FirstOrDefault(e => e.EmployeeEmail == email);
            if(user != null)
            {
                ICryptoService cryptoService = new PBKDF2();

                string hashed = cryptoService.Compute(password, user.Salt);

                if (hashed == user.Pass)
                {
                    Session["userLoggedIn"] = user.EmployeeEmail;
                    return RedirectToAction("Index", "Students");

                }
            }
            
            return View();
        }


        public ActionResult Logout()
        {
            Session["userLoggedIn"] = null;
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            ViewBag.message = ""; 
            return View(); 
        }

        [HttpPost]
        public ActionResult ResetPassword(string oldPassword, string newPassword, string email)
        {
            //check if old password matches
            var user = db.Employees.FirstOrDefault(e => e.EmployeeEmail == email);
            if (user != null)
            {
                ICryptoService cryptoService = new PBKDF2();

                string hashed = cryptoService.Compute(oldPassword, user.Salt);

                if (hashed == user.Pass) //if the old password is correct
                {
                    //generate a new salt
                    string salt = cryptoService.GenerateSalt();
                    user.Salt = salt;

                    //hash new password
                    string hashedPassword = cryptoService.Compute(newPassword);
                    user.Pass = hashedPassword;

                    db.SaveChanges();
                    //log the user in
                    Session["userLoggedIn"] = user.EmployeeEmail;
                    return RedirectToAction("Index", "Interviews");

                }
                else
                {
                    ViewBag.message = "Old password is incorrect, please try again";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "No user with that email address exists, please try again";
                return View();            
            }


        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            //check if user with that email exists
            var user = db.Employees.FirstOrDefault(e => e.EmployeeEmail == email);
            if (user != null)
            {

            }
                //send an email to that user with their password.  (build smtp function)
                return View();
        }

        #endregion
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