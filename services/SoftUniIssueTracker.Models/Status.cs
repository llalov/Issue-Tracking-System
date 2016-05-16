using System.ComponentModel.DataAnnotations.Schema;
using SIT.Models.Interfaces;

namespace SIT.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Status : IDentificatable
    {
        private ICollection<Issue> issues;
        private ICollection<StatusTransition> childStatuses;
        private ICollection<StatusTransition> parentStatuses;  

        public Status()
        {
            this.issues = new HashSet<Issue>();

        }

        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<Issue> Issues
        {
            get { return this.issues; }
            set { this.issues = value; }
        }

        [InverseProperty("ChildStatus")]
        public virtual ICollection<StatusTransition> ChildStatuses
        {
            get { return this.childStatuses; }
            set { this.childStatuses = value; }
        }

        [InverseProperty("ParentStatus")]
        public virtual ICollection<StatusTransition> ParentStatuses
        {
            get { return this.parentStatuses; }
            set { this.parentStatuses = value; }
        }
    }
}