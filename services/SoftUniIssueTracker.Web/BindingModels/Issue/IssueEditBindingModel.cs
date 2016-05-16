using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;

namespace SIT.Web.BindingModels.Issue
{
    public class IssueEditBindingModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string AssigneeId { get; set; }

        [Required]
        public int PriorityId { get; set; }

        public List<Label> Labels { get; set; }
    }
}
