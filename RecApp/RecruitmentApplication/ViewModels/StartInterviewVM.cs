using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentApplication.Models;
using System.Text.RegularExpressions;

namespace RecruitmentApplication.ViewModels
{
    public class StartInterviewVM
    {
        #region Display Fields

        [Display(Name = "Panel Members")]
        public List<PanelMember> panelMembers { get; set; }

        [Display(Name = "Bio")]
        public string bio { get; set; }

        [Display(Name = "Date of Birth")]
        public string dateOfBirth { get; set; }

        [Display(Name = "Date")]
        public DateTime interviewDate { get; set; }

        [Display(Name = "Room")]
        public string roomNumber { get; set; }

        [Required(ErrorMessage = "Please enter an overall panel score from 1-10")]
        [Display(Name = "Panel Score")]
        public int panelScore { get; set; }

        [Display(Name = "Overall Score")]
        public decimal overallScore { get; set; }

        [Required(ErrorMessage = "Please enter an overall comment")]
        [Display(Name = "Overall Comment")]
        public string overallComment { get; set; }

        [Display(Name = "Panel Member Name")]
        public List<Employee> employees { get; set; }

        #endregion

        #region Objects

        public Student Student { get; set; }

        public InterviewSession session { get; set; }

        public Interview interview { get; set; }

        //scoring stuff

        public List<TraitCategory> categories { get; set; }

        public List<TraitComment> comments { get; set; }

        #endregion

        #region Format Methods
        public string bioRegex(string bio)
        {
            int lineLength = 20;
            return bio = Regex.Replace(bio, "(.{" + lineLength + "})", "$1" + Environment.NewLine);
        }

        public string dobFormat(DateTime dob)
        {
            return  dateOfBirth = dob.ToString("D");
        }

        #endregion


    }
}