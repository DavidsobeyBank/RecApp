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
    
    public partial class PanelMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PanelMember()
        {
            this.TraitComments = new HashSet<TraitComment>();
        }
    
        public int PanelID { get; set; }
        public int EmployeeID { get; set; }
        public int InterviewID { get; set; }
        public int PannelScore { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Interview Interview { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TraitComment> TraitComments { get; set; }
    }
}
