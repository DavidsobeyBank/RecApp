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
        public IEnumerable<SelectListItem> panelMembers;

        [Display(Name = "Bio")]
        public string bio { get; set; }

        [Display(Name = "Date of Birth")]
        public string dateOfBirth { get; set; }

        [Display(Name = "Date")]
        public DateTime interviewDate { get; set; }

        [Display(Name = "Room")]
        public string roomNumber { get; set; }

        #endregion

        #region Objects
        public List<int> selectedEmployeeIDs { get; set; }

        public Student Student { get; set; }

        public InterviewSession session { get; set; }

        public Interview interview { get; set; }

        public List<PanelMember> panel { get; set; }

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