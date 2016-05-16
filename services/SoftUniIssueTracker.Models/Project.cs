using SIT.Models.Interfaces;

namespace SIT.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Project : IDentificatable
    {
        private ICollection<ProjectPriority> projectPriorities;
        private ICollection<ProjectLabel> projectLabels;
        private ICollection<Issue> issues;

        public Project()
        {
            this.projectPriorities = new HashSet<ProjectPriority>();
            this.projectLabels = new HashSet<ProjectLabel>();
            this.issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } 

        [Required]
        public string ProjectKey { get; set; }

        public string Description { get; set; }

        [Required]
        public string LeadId { get; set; }

        public virtual User Lead { get; set; }

        public int TransitionSchemeId { get; set; }

        public virtual TransitionScheme TransitionScheme { get; set; }

        public virtual ICollection<ProjectPriority> ProjectPriorities
        {
            get { return this.projectPriorities; }
            set { this.projectPriorities = value; }
        }

        public virtual ICollection<ProjectLabel> ProjectLabels
        {
            get { return this.projectLabels; }
            set { this.projectLabels = value; }
        }

        public virtual ICollection<Issue> Issues
        {
            get { return this.issues; }
            set { this.issues = value; }
        }
    }
}