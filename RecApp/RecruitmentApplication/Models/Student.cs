//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecruitmentApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.Interviews = new HashSet<Interview>();
        }
    
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public System.DateTime StudentDOB { get; set; }
        public string StudentUniversity { get; set; }
        public string StudentDegree { get; set; }
        public string StudentYearofStudy { get; set; }
        public string StudentPhoto { get; set; }
        public string StudentBio { get; set; }
        public string StudentVideo { get; set; }

        public string StudentBirth()
        {
            return StudentDOB.ToString("D");
        }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
