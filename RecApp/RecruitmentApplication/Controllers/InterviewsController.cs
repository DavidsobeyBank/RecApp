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

        [HttpGet]
        public ActionResult StartInterview(int? id)
        {
            StartInterviewVM startInterviewVM = new StartInterviewVM();
            Interview interview = db.Interviews.Find(id);
            
            if (interview != null)
            {
                Session["currentInterview"] = interview.InterviewID.ToString();
                //make a new startInterviewVM intance and assign to it
                startInterviewVM.interview = interview;
                startInterviewVM.Student = interview.Student;
                startInterviewVM.session = interview.InterviewSession;
                startInterviewVM.bio = startInterviewVM.bioRegex(interview.Student.StudentBio); 
                startInterviewVM.dateOfBirth = startInterviewVM.dobFormat(interview.InterviewDate);

                startInterviewVM.panelMembers = new SelectList(db.Employees, "EmployeeID", "EmployeeName", startInterviewVM.selectedEmployeeIDs);
                
            }

            return View(startInterviewVM);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StartInterview(StartInterviewVM model)
        {
            try
            {
                if (model.selectedEmployeeIDs.Count() == 0)
                {
                    return View(model);
                }

                List<int> employeeIds = new List<int>();
                employeeIds = model.selectedEmployeeIDs;

                //assign interview to panel members
                PanelMembersController panelMembersController = new PanelMembersController();
                String interviewId = Session["currentInterview"].ToString();
                
                if (!string.IsNullOrEmpty(interviewId))
                {
                    int idVal = Convert.ToInt32(interviewId);
                    Interview interview = db.Interviews.Find(idVal);

                    //create a panel member for each employee selected
                    model.panel = panelMembersController.AssignInterview(interview.InterviewID, employeeIds);

                    foreach (PanelMember p in model.panel)
                    {
                        interview.PanelMembers.Add(p);
                    }
                    //update interview status

                    //save the updated interview //have to change multiplicity of FK_panel_interview to allow many panel members
                    db.SaveChanges();
                    //open trait category modal

                    //enable trait scoring and comments
                }
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine("Aw shuck we done messed up " + ex.Message);
            }
            return View(model);

        }
        // GET: Interviews/Create
        public ActionResult Create()
        {
            CreateInterviewVM model = new CreateInterviewVM();

            model.sessions = new SelectList(db.InterviewSessions, "SessionID", "SessionName");
            model.students = new SelectList(db.Students, "StudentID", "StudentName");

            return View(model);
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInterviewVM model)
        {
            Interview interview = new Interview();

            if (ModelState.IsValid)
            {
                string student = model.studentID;
                string session = model.sessionID;

                interview.StudentID = Convert.ToInt32(student);
                interview.SessionID = Convert.ToInt32(session);
                interview.InterviewDate = model.interviewDate;
                interview.Room = model.roomNumber;
                interview.OverallComment = "Interview not yet started";
                

                db.Interviews.Add(interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.students = new SelectList(db.Students, "StudentID", "StudentName", model.studentID);
            ViewBag.sessions = new SelectList(db.InterviewSessions, "SessionID", "SessionName", model.sessionID);

            return View(model);
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
