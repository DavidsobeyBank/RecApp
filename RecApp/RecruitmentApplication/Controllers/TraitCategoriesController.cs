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
    public class TraitCategoriesController : Controller
    {
        private RecruitmentAppEntities db = new RecruitmentAppEntities();

        // GET: TraitCategories
        public ActionResult Index()
        {
            return View(db.TraitCategories.ToList());
        }

        // GET: TraitCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraitCategory traitCategory = db.TraitCategories.Find(id);
            if (traitCategory == null)
            {
                return HttpNotFound();
            }
            return View(traitCategory);
        }

        // GET: TraitCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TraitCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraitID,TraitName,TraitDescription")] TraitCategory traitCategory)
        {
            if (ModelState.IsValid)
            {
                db.TraitCategories.Add(traitCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(traitCategory);
        }

        // GET: TraitCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraitCategory traitCategory = db.TraitCategories.Find(id);
            if (traitCategory == null)
            {
                return HttpNotFound();
            }
            return View(traitCategory);
        }

        // POST: TraitCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraitID,TraitName,TraitDescription")] TraitCategory traitCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traitCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(traitCategory);
        }

        // GET: TraitCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraitCategory traitCategory = db.TraitCategories.Find(id);
            if (traitCategory == null)
            {
                return HttpNotFound();
            }
            return View(traitCategory);
        }

        // POST: TraitCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TraitCategory traitCategory = db.TraitCategories.Find(id);
            db.TraitCategories.Remove(traitCategory);
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
