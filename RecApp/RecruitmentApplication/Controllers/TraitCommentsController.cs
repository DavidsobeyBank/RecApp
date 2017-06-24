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
    public class TraitCommentsController : Controller
    {
        private RecruitmentAppEntities db = new RecruitmentAppEntities();

        // GET: TraitComments
        public ActionResult Index()
        {
            var traitComments = db.TraitComments.Include(t => t.PanelMember).Include(t => t.TraitCategory);
            return View(traitComments.ToList());
        }

        // GET: TraitComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraitComment traitComment = db.TraitComments.Find(id);
            if (traitComment == null)
            {
                return HttpNotFound();
            }
            return View(traitComment);
        }

        // GET: TraitComments/Create
        public ActionResult Create()
        {
            ViewBag.PanelID = new SelectList(db.PanelMembers, "PanelID", "PanelID");
            ViewBag.TraitID = new SelectList(db.TraitCategories, "TraitID", "TraitName");
            return View();
        }

        // POST: TraitComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraitCommentID,PanelID,TraitID,Comment,Score")] TraitComment traitComment)
        {
            if (ModelState.IsValid)
            {
                db.TraitComments.Add(traitComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PanelID = new SelectList(db.PanelMembers, "PanelID", "PanelID", traitComment.PanelID);
            ViewBag.TraitID = new SelectList(db.TraitCategories, "TraitID", "TraitName", traitComment.TraitID);
            return View(traitComment);
        }

        // GET: TraitComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraitComment traitComment = db.TraitComments.Find(id);
            if (traitComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PanelID = new SelectList(db.PanelMembers, "PanelID", "PanelID", traitComment.PanelID);
            ViewBag.TraitID = new SelectList(db.TraitCategories, "TraitID", "TraitName", traitComment.TraitID);
            return View(traitComment);
        }

        // POST: TraitComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraitCommentID,PanelID,TraitID,Comment,Score")] TraitComment traitComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traitComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PanelID = new SelectList(db.PanelMembers, "PanelID", "PanelID", traitComment.PanelID);
            ViewBag.TraitID = new SelectList(db.TraitCategories, "TraitID", "TraitName", traitComment.TraitID);
            return View(traitComment);
        }

        // GET: TraitComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraitComment traitComment = db.TraitComments.Find(id);
            if (traitComment == null)
            {
                return HttpNotFound();
            }
            return View(traitComment);
        }

        // POST: TraitComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TraitComment traitComment = db.TraitComments.Find(id);
            db.TraitComments.Remove(traitComment);
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
