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

namespace RecruitmentApplication.Controllers
{
    public class InterviewsController : Controller
    {
        private RecruitmentAppEntities db = new RecruitmentAppEntities();

        // GET: Interviews
        public ActionResult Index()
        {
            var interviews = db.Interviews.Include(i => i.InterviewSession).Include(i => i.Student);
            return View(interviews.ToList());
        }

        // GET: Interviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        public ActionResult StartInterview(int? id)
        {
            Interview interview = db.Interviews.Find(id);

            return View(interview);
        }
        // GET: Interviews/Create
        public ActionResult Create()
        {
            ViewBag.Session = new SelectList(db.InterviewSessions, "SessionID", "SessionName");
            ViewBag.Student = new SelectList(db.Students, "StudentID", "StudentName");
            return View();
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterviewID,InterviewDate,Room")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                interview.StudentID = Convert.ToInt32(Request.Form["drpStudents"].ToString());

                interview.SessionID = Convert.ToInt32(Request.Form["drpSession"].ToString());
                
                db.Interviews.Add(interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Session = new SelectList(db.InterviewSessions, "SessionID", "SessionDescription", interview.SessionID);
            ViewBag.Student = new SelectList(db.Students, "StudentID", "StudentName", interview.StudentID);
            return View(interview);
        }

        // GET: Interviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            ViewBag.SessionID = new SelectList(db.InterviewSessions, "SessionID", "SessionDescription", interview.SessionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", interview.StudentID);
            return View(interview);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterviewID,StudentID,InterviewDate,SessionID,Room,OverallComment,OverallScore")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SessionID = new SelectList(db.InterviewSessions, "SessionID", "SessionDescription", interview.SessionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", interview.StudentID);
            return View(interview);
        }

        // GET: Interviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interview interview = db.Interviews.Find(id);
            db.Interviews.Remove(interview);
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
