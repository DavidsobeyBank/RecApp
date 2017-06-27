using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecruitmentApplication.Models;

namespace RecruitmentApplication.Controllers
{
    public class PanelMembersController : Controller
    {
        private RecruitmentAppEntities db = new RecruitmentAppEntities();

        // GET: PanelMembers
        public ActionResult Index()
        {
            var panelMembers = db.PanelMembers.Include(p => p.Employee).Include(p => p.Interview);
            return View(panelMembers.ToList());
        }

        public List<PanelMember> AssignInterview(int interviewId, List<int> employeeIds)
        {
            List<PanelMember> panel = new List<PanelMember>();
            try
            {
                //check if the interview actually exists
                Interview interview = db.Interviews.FirstOrDefault(i => i.InterviewID == interviewId);
                if (interview != null)
                {
                    foreach (int id in employeeIds)
                    {
                        //get employee details
                        Employee emp = db.Employees.FirstOrDefault(p => p.EmployeeID == id);
                        if (emp != null)
                        {
                            //create new panel members from employee and interview details
                            PanelMember panelMember = new PanelMember();
                            panelMember.EmployeeID = emp.EmployeeID;
                            panelMember.InterviewID = interview.InterviewID;

                            db.PanelMembers.Add(panelMember);
                            panel.Add(panelMember);
                            db.SaveChanges(); 
                        }
                    }
                }
                
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine("Aw shucks, we couldn't add all the interview to all the panel members " + ex.Message);
            }

            return panel; 


        }
        // GET: PanelMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PanelMember panelMember = db.PanelMembers.Find(id);
            if (panelMember == null)
            {
                return HttpNotFound();
            }
            return View(panelMember);
        }

        // GET: PanelMembers/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName");
            ViewBag.InterviewID = new SelectList(db.Interviews, "InterviewID", "Room");
            return View();
        }

        // POST: PanelMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PanelID,EmployeeID,InterviewID,PannelScore")] PanelMember panelMember)
        {
            if (ModelState.IsValid)
            {
                db.PanelMembers.Add(panelMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName", panelMember.EmployeeID);
            ViewBag.InterviewID = new SelectList(db.Interviews, "InterviewID", "Room", panelMember.InterviewID);
            return View(panelMember);
        }

        // GET: PanelMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PanelMember panelMember = db.PanelMembers.Find(id);
            if (panelMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName", panelMember.EmployeeID);
            ViewBag.InterviewID = new SelectList(db.Interviews, "InterviewID", "Room", panelMember.InterviewID);
            return View(panelMember);
        }

        // POST: PanelMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PanelID,EmployeeID,InterviewID,PannelScore")] PanelMember panelMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panelMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName", panelMember.EmployeeID);
            ViewBag.InterviewID = new SelectList(db.Interviews, "InterviewID", "Room", panelMember.InterviewID);
            return View(panelMember);
        }

        // GET: PanelMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PanelMember panelMember = db.PanelMembers.Find(id);
            if (panelMember == null)
            {
                return HttpNotFound();
            }
            return View(panelMember);
        }

        // POST: PanelMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PanelMember panelMember = db.PanelMembers.Find(id);
            db.PanelMembers.Remove(panelMember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
