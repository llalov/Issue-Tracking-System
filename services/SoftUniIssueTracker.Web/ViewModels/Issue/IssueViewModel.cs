using System;
using System.Collections.Generic;
using SIT.Models;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.Priority;
using SIT.Web.ViewModels.Project;
using SIT.Web.ViewModels.Status;
using SIT.Web.ViewModels.User;

namespace SIT.Web.ViewModels.Issue
{
    public class IssueViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IssueKey { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ProjectIssueViewModel Project { get; set; }
        public UserViewModel Author { get; set; }
        public UserViewModel Assignee { get; set; }
        public PriorityViewModel Priority { get; set; }
        public StatusViewModel Status { get; set; }
        public IEnumerable<LabelViewModel> Labels { get; set; }
        public IEnumerable<StatusViewModel> AvailableStatuses { get; set; }
    }
}
