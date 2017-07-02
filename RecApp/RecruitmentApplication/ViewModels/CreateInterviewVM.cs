using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentApplication.Models;

namespace RecruitmentApplication.ViewModels
{
    public class CreateInterviewVM
    {
        #region Display properties

        [Display(Name = "Student")]
        public IEnumerable<SelectListItem> students;

        [Display(Name = "Date")]
        public DateTime interviewDate { get; set; }

        [Display(Name = "Room")]
        public string roomNumber { get; set; }

        [Display(Name = "Session")]
        public IEnumerable<SelectListItem> sessions;

        public List<Employee> EmplList { get; set; }

        #endregion

        public string studentID { get; set; }

        public string sessionID { get; set; }

    }
}