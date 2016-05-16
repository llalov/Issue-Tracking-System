using SIT.Models.Interfaces;

namespace SIT.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Label : IDentificatable
    {
        private ICollection<ProjectLabel> projectLabels;
        private ICollection<IssueLabel> issueLabels; 

        public Label()
        {
            this.projectLabels = new HashSet<ProjectLabel>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProjectLabel> Projects
        {
            get { return this.projectLabels; }
            set { this.projectLabels = value; }
        }

        public virtual ICollection<IssueLabel> Issues
        {
            get { return this.issueLabels; }
            set { this.issueLabels = value; }
        } 
    }
}