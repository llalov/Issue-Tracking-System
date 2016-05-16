using SIT.Models.Interfaces;

namespace SIT.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Priority : IDentificatable
    {
        private ICollection<ProjectPriority> projectPriorities;
        private ICollection<Issue> issues; 

        public Priority()
        {
            this.projectPriorities = new HashSet<ProjectPriority>();
            this.issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProjectPriority> Projects
        {
            get { return this.projectPriorities; }
            set { this.projectPriorities = value; }
        }

        public virtual ICollection<Issue> Issues
        {
            get { return this.issues; }
            set { this.issues = value; }
        } 
    }
}