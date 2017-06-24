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
    public class InterviewSessionsController : Controller
    {
        private RecruitmentAppEntities db = new RecruitmentAppEntities();

        // GET: InterviewSessions
        public ActionResult Index()
        {
            return View(db.InterviewSessions.ToList());
        }

        // GET: InterviewSessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewSession interviewSession = db.InterviewSessions.Find(id);
            if (interviewSession == null)
            {
                return HttpNotFound();
            }
            return View(interviewSession);
        }

        // GET: InterviewSessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InterviewSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionID,SessionDescription,SessionName")] InterviewSession interviewSession)
        {
            if (ModelState.IsValid)
            {
                db.InterviewSessions.Add(interviewSession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interviewSession);
        }

        // GET: InterviewSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewSession interviewSession = db.InterviewSessions.Find(id);
            if (interviewSession == null)
            {
                return HttpNotFound();
            }
            return View(interviewSession);
        }

        // POST: InterviewSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionID,SessionDescription,SessionName")] InterviewSession interviewSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interviewSession);
        }

        // GET: InterviewSessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewSession interviewSession = db.InterviewSessions.Find(id);
            if (interviewSession == null)
            {
                return HttpNotFound();
            }
            return View(interviewSession);
        }

        // POST: InterviewSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewSession interviewSession = db.InterviewSessions.Find(id);
            db.InterviewSessions.Remove(interviewSession);
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
