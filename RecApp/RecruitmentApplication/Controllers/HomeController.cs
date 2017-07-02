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

        public ActionResult Index()
        {
            return View();
        }

        #region Login Stuff

        [HttpPost]
        public ActionResult Login()
        {
            string email = Request["Email"].ToString();
            string password = Request["Password"].ToString();

            //check if user exists and password matches
            var user = db.Employees.FirstOrDefault(e => e.EmployeeEmail == email);
            if(user != null)
            {
                ICryptoService cryptoService = new PBKDF2();
                
                string enteredPassword = cryptoService.Compute(password);

                //validate user
                //compare the password (this should be true since we are rehashing the same password and using the same generated salt)
                string storedPassword = cryptoService.Compute(user.Pass, user.Salt);
                bool isPasswordValid = cryptoService.Compare(enteredPassword, storedPassword);

                if(isPasswordValid)
                {

                    //login
                }
                else
            }
            
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