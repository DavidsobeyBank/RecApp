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
        [Required(ErrorMessage = "Please select a date")]
        public DateTime interviewDate { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "Pelase enter a room number")]
        public string roomNumber { get; set; }

        [Display(Name = "Session")]
        public IEnumerable<SelectListItem> sessions;

        #endregion

        public string studentID { get; set; }

        public string sessionID { get; set; }


    }
}