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
        [Display(Name = "Student")]
        public IEnumerable<SelectListItem> students;

        public string studentID { get; set; }

        public string sessionID { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please select a date")]
        public DateTime interviewDate { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "Pelase enter a room number")]
        public string roomNumber { get; set; }

        [Display(Name = "Session")]
        public IEnumerable<SelectListItem> sessions;
    }
}