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
                try
                {
                    startInterviewVM.interview = interview;
                    startInterviewVM.Student = interview.Student;
                    startInterviewVM.session = interview.InterviewSession;
                    startInterviewVM.bio = startInterviewVM.bioRegex(interview.Student.StudentBio);
                    startInterviewVM.dateOfBirth = startInterviewVM.dobFormat(interview.InterviewDate);
                    startInterviewVM.panelMembers = new List<PanelMember>();
                    startInterviewVM.employees = new List<Employee>();
                    //get all panelMembers for this interview
                    var panelMembers = db.PanelMembers.ToList().Where(p => p.InterviewID == interview.InterviewID);

                    foreach (PanelMember p in panelMembers)
                    {
                        startInterviewVM.panelMembers.Add(p);
                        startInterviewVM.employees.Add(p.Employee);
                    }
                    startInterviewVM.categories = new List<TraitCategory>(db.TraitCategories);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("messed up yo" + ex.Message);
                }

            }

            return View(startInterviewVM);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StartInterview(StartInterviewVM model)
        {
            try
            {

                String interviewID = ""; 
                if(Session["CurrentInterview"] != null)
                {
                    interviewID = Session["currentInterview"].ToString();
                }

                int idVal = Convert.ToInt32(interviewID);
                Interview interview = db.Interviews.Find(idVal);

                //get all panelMembers for this interview
                model.panelMembers = new List<PanelMember>();

                var panelMembers = db.PanelMembers.ToList().Where(p => p.InterviewID == interview.InterviewID);
                model.employees = new List<Employee>();

                foreach (PanelMember p in panelMembers)
                {
                    model.panelMembers.Add(p);
                    model.employees.Add(p.Employee);
                }

                //add all category comments and scores to the db

                interview.OverallComment = model.overallComment;

                foreach (TraitCategory category in db.TraitCategories)
                {
                    string name = category.TraitName; 
                    TraitComment comment = new TraitComment();
                    string score = "" ;
                    string commentText = "";

                    if(Request[name + "Score"] != null)
                    {
                        score = Request[name + "Score"].ToString();
                    }

                    comment.Score = Convert.ToInt32(score);

                    if (Request[name + "Comment"] != null)
                    {
                        commentText = Request[name + "Comment"].ToString();
                    }

                    comment.Comment = commentText;

                    comment.TraitID = category.TraitID;
                    comment.TraitCategory = category;

                    comment.PanelMember = model.panelMembers.FirstOrDefault();
                    comment.PanelID = comment.PanelMember.PanelID;

                    db.TraitComments.Add(comment);
                }

                //add the panel member's score to the appropriate row
                var member = model.panelMembers.First();

                member.PannelScore = model.panelScore;

                interview.OverallScore = totalScoreCalc();
                ////change the interview status to completed 
                //interview.StatusID = 3;

                db.SaveChanges(); 

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Aw shucks we done messed up " + ex.Message);
            }



            return View(model);

        }

        public decimal totalScoreCalc(List<int> panelScores)
        {
            int sum = 0;
            int overallScore = 0;
            int count = panelScores.Count;

            if (count > 0)
            {
                foreach (int score in panelScores)
                {
                    sum = sum + score;
                }

                overallScore = sum / count;
            }

            return overallScore;
        }
        // GET: Interviews/Create
        public ActionResult Create()
        {
            CreateInterviewVM model = new CreateInterviewVM();

            model.sessions = new SelectList(db.InterviewSessions, "SessionID", "SessionName");
            model.students = new SelectList(db.Students, "StudentID", "StudentName");
            model.panelMembers = new SelectList(db.Employees, "EmployeeID", "EmployeeName");

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
                string employee = model.panelMemberID;
                int empID = Convert.ToInt32(employee);
                Employee emp = db.Employees.FirstOrDefault(e => e.EmployeeID == empID);

                interview.StudentID = Convert.ToInt32(student);
                interview.SessionID = Convert.ToInt32(session);
                interview.InterviewDate = model.interviewDate;
                interview.Room = model.roomNumber;
                interview.OverallComment = "Interview not yet started";
                
                db.Interviews.Add(interview);

                PanelMember member = new PanelMember();
                member.InterviewID = interview.InterviewID;
                member.Interview = interview;
                if(emp != null)
                {
                    member.EmployeeID = emp.EmployeeID;
                    member.Employee = emp;
                }

                db.PanelMembers.Add(member);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.students = new SelectList(db.Students, "StudentID", "StudentName", model.studentID);
            model.sessions = new SelectList(db.InterviewSessions, "SessionID", "SessionName", model.sessionID);
            model.panelMembers = new SelectList(db.Employees, "EmployeeID", "EmployeeName", model.panelMemberID);

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
