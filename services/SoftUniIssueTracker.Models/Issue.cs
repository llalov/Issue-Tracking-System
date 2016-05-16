using SIT.Models.Interfaces;

namespace SIT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Issue : IDentificatable
    {
        private ICollection<IssueLabel> _issueLabels;
        private ICollection<Comment> comments;

        public Issue()
        {
            this._issueLabels = new HashSet<IssueLabel>();
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string IssueKey { get; set; } 

        [Required]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string AssigneeId { get; set; }

        public virtual User Assignee { get; set; }

        public int PriorityId { get; set; }

        public virtual Priority Priority { get; set; }

        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<IssueLabel> IssueLabels
        {
            get { return this._issueLabels; }
            set { this._issueLabels = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}