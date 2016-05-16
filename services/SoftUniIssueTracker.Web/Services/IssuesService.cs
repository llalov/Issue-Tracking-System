using System;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.Services.Interfaces;
using SIT.Web.ViewModels.Issue;
using System.Linq;
using System.Linq.Dynamic;
using SIT.Web.BindingModels.Comment;
using SIT.Web.BindingModels.Issue;
using SIT.Web.ViewModels.Comment;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.Status;
namespace SIT.Web.Services
{
    public class IssuesService : BaseService, IIssuesService
    {
        public IssuesService(ISoftUniIssueTrackerData data, IMapper mapper) : base(data, mapper)
        {
        }

        public IssueViewModel Add(string authorId, IssueBindingModel model)
        {
            var issue = mapper.Map<IssueBindingModel, Issue>(model);

            var issueProject = this.data.ProjectRepository.GetById(issue.ProjectId)
                .Include(p => p.Issues)
                .Include(i => i.ProjectLabels)
                .Include(p => p.ProjectPriorities.Select(pp => pp.Priority))
                .FirstOrDefault();
            if (issueProject == null)
            {
                throw new ArgumentException(Constants.UnexistingProjectErrorMessage);
            }

            var user = this.data.UserRepository.GetById(authorId);
            if (issueProject.LeadId != user.Id && !user.isAdmin)
            {
                throw new InvalidOperationException(Constants.NotProjectLeaderOrAdmin);
            }

            var issueAssignee = this.data.UserRepository.GetById(model.AssigneeId);
            if (issueAssignee == null)
            {
                throw new ArgumentException(Constants.UnexistingUserErrorMessage);
            }

            var issuePriority =
                this.data.ProjectPrioritiesRepository.Get(pp => pp.ProjectId == issue.ProjectId
                                                                && pp.PriorityId == issue.PriorityId).FirstOrDefault();
            if (issuePriority == null)
            {
                throw new ArgumentException(Constants.UnexistingPriorityForProjectErrorMessage);
            }

            var projectIssuesCount = issueProject.Issues.Count;
            issue.IssueKey = issueProject.ProjectKey + "-" + ++projectIssuesCount;

            issue.AuthorId = authorId;
            SelectParentStatusInTransitionScheme(issueProject.TransitionSchemeId, issue);

            if (model.Labels != null)
                AddLabels(model.Labels, issue);

            this.data.IssueRepository.Insert(issue);
            this.data.Save();

            return GetMappedIssueWithAvailableStatuses(issue);
        }

        public IssueViewModel Edit(string userId, int id, IssueEditBindingModel model)
        {
            var issue = this.data.IssueRepository.GetById(id)
                .Include(i => i.IssueLabels.Select(il => il.Label))
                .Include(i => i.Priority)
                .Include(i => i.Project)
                .Include(i => i.Status)
                .FirstOrDefault();
            if (issue == null)
            {
                throw new ArgumentException(Constants.UnexistingProjectErrorMessage);
            }

            var user = this.data.UserRepository.GetById(userId);
            if (issue.Project.LeadId != user.Id && !user.isAdmin)
            {
                throw new InvalidOperationException(Constants.NotProjectLeaderOrAdmin);
            }

            issue.Title = model.Title;
            issue.Description = model.Description;
            issue.AssigneeId = model.AssigneeId;
            issue.PriorityId = model.PriorityId;
            issue.DueDate = model.DueDate;

            var labels = this.data.IssueLabelsRepository.Get(il => il.IssueId == issue.Id).ToList();
            foreach (var issueLabel in labels)
            {
                this.data.IssueLabelsRepository.Delete(issueLabel);
            }

            if (model.Labels != null)
                AddLabels(model.Labels, issue);

            this.data.Save();

            return GetMappedIssueWithAvailableStatuses(issue);
        }

        public IssueWithPagesViewModel Get(int pageSize, int pageNumber, string filter)
        {
            var issuesQuery = this.data.IssueRepository.Get()
                .Include(i => i.Assignee)
                .Include(i => i.Author)
                .Include(i => i.Priority)
                .Include(i => i.Status)
                .Include(i => i.Project)
                .Include(i => i.IssueLabels.Select(il => il.Label))
                .OrderBy(i => i.Id)
                .AsQueryable();

            if (filter != null)
            {
                issuesQuery = issuesQuery.Where(filter);
            }

            var totalCount = issuesQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var issues = issuesQuery.Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            var mappedIssues = GetMappedIssuesWithAvailableStatuses(issues);

            return new IssueWithPagesViewModel()
            {
                TotalPages = totalPages,
                Issues = mappedIssues,
                TotalCount = totalCount
            };
        }

        public IssueWithPagesViewModel GetUserIssues(string userId, int pageSize, int pageNumber, string orderBy)
        {
            var issuesQuery = this.data.IssueRepository.Get(i => i.AssigneeId == userId)
                .Include(i => i.Assignee)
                .Include(i => i.Author)
                .Include(i => i.Priority)
                .Include(i => i.Status)
                .Include(i => i.Project)
                .Include(i => i.IssueLabels.Select(il => il.Label))
                .AsQueryable();

            if (orderBy != null)
            {
                issuesQuery = issuesQuery.OrderBy(orderBy);
            }

            var totalCount = issuesQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var issues = issuesQuery.Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            var mappedIssues = GetMappedIssuesWithAvailableStatuses(issues);

            return new IssueWithPagesViewModel()
            {
                TotalPages = totalPages,
                Issues = mappedIssues,
                TotalCount = totalCount
            };
        }

        public IEnumerable<IssueViewModel> GetProjectIssues(int projectId)
        {
            var issues = this.data.IssueRepository.Get(i => i.ProjectId == projectId)
                .Include(i => i.Assignee)
                .Include(i => i.Author)
                .Include(i => i.Priority)
                .Include(i => i.Status)
                .Include(i => i.Project)
                .Include(i => i.IssueLabels.Select(il => il.Label))
                .ToList();

            return GetMappedIssuesWithAvailableStatuses(issues);
        }

        public IssueViewModel GetById(int id)
        {
            var issue = this.data.IssueRepository.GetById(id)
                .Include(i => i.Assignee)
                .Include(i => i.Author)
                .Include(i => i.Priority)
                .Include(i => i.Status)
                .Include(i => i.Project)
                .Include(i => i.IssueLabels.Select(il => il.Label))
                .FirstOrDefault();
            if (issue == null)
            {
                throw new ArgumentException(Constants.UnexistingIssueErrorMessage);
            }

            return GetMappedIssueWithAvailableStatuses(issue);
        }

        public IEnumerable<StatusViewModel> ChangeStatus(string userId, int issueId, int statusId)
        {
            var issue = this.data.IssueRepository.GetById(issueId)
                .Include(i => i.Project)
                .FirstOrDefault();
            if (issue == null)
            {
                throw new ArgumentException(Constants.UnexistingIssueErrorMessage);
            }

            var user = this.data.UserRepository.GetById(userId);
            if (issue.Project.LeadId != user.Id && !user.isAdmin && issue.AssigneeId != user.Id)
            {
                throw new InvalidOperationException(Constants.NotProjectLeaderOrAdminOrAssignee);
            }

            var status = this.data.StatusRepository.GetById(statusId).FirstOrDefault();
            if (status == null)
            {
                throw new ArgumentException(Constants.UnexistingStatusErrorMessage);
            }

            var statusInIssueTransitionScheme =
                this.data.StatusTransitionRepository.Get(st => st.TransitionSchemeId == issue.Project.TransitionSchemeId
                                                               && st.ChildStatusId == status.Id
                                                               && st.ParentStatusId == issue.StatusId)
                    .FirstOrDefault();
            if (statusInIssueTransitionScheme == null)
            {
                throw new ArgumentException(Constants.UnavailableStatusForIssue);
            }

            issue.StatusId = statusId;
            this.data.Save();

            return GetAvailableStatuses(statusId, issue.Project.TransitionSchemeId);
        }

        public IEnumerable<CommentViewModel> AddComment(int issueId, string authorId, CommentBindingModel model)
        {
            var comment = mapper.Map<CommentBindingModel, Comment>(model);

            var issue = this.data.IssueRepository.GetById(issueId)
                .Include(i => i.Project)
                .FirstOrDefault();
            if (issue == null)
            {
                throw new ArgumentException(Constants.UnexistingIssueErrorMessage);
            }

            var user = this.data.UserRepository.GetById(authorId);
            var projectIssuesLeads = this.data.ProjectRepository.GetById(issue.ProjectId)
                .Select(p => p.Issues.Select(i => i.AssigneeId)).FirstOrDefault();
            if (issue.Project.LeadId != user.Id && 
                projectIssuesLeads != null && 
                !projectIssuesLeads.Contains(user.Id) &&
                user.isAdmin == false)
            {
                throw new InvalidOperationException(Constants.NotAssociatedWithIssue);
            }

            comment.Issue = issue;
            comment.AuthorId = authorId;
            comment.CreatedOn = DateTime.UtcNow;

            this.data.CommentRepository.Insert(comment);
            this.data.Save();

            return GetIssueComments(issueId);
        }

        public IEnumerable<CommentViewModel> GetIssueComments(int id)
        {
            var issue = this.data.IssueRepository.GetById(id).FirstOrDefault();
            if (issue == null)
            {
                throw new ArgumentException(Constants.UnexistingIssueErrorMessage);
            }

            var comments = this.data.CommentRepository.Get(c => c.IssueId == id)
                .Include(c => c.Author)
                .ToList();
            return mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
        }

        private IEnumerable<StatusViewModel> GetAvailableStatuses(int statusId, int transitionSchemeId)
        {
            var availableStatuses = this.data.StatusTransitionRepository.Get(
                st => st.TransitionSchemeId == transitionSchemeId
                      && st.ParentStatusId == statusId)
                .Select(st => st.ChildStatus)
                .ToList();

            return availableStatuses.Select(s => mapper.Map<Status, StatusViewModel>(s));
        }

        //TODO: Extract method in base class because it is duplicated in the ProjectService
        private void AddLabels(IEnumerable<Label> labels, Issue issue)
        {
            foreach (var label in labels)
            {
                var labelEntity = this.data.LabelRepository.Get(l => l.Name == label.Name).FirstOrDefault();
                if (labelEntity == null)
                {
                    labelEntity = new Label()
                    {
                        Name = label.Name
                    };
                    this.data.LabelRepository.Insert(labelEntity);
                }

                var issueLabel = new IssueLabel()
                {
                    Label = labelEntity,
                    Issue = issue
                };

                //var projectLabelEntity = this.data.ProjectLabelsRepository.Get(e => e.LabelId == labelEntity.Id
                //                                                            && e.ProjectId == project.Id).FirstOrDefault();
                //if (projectLabelEntity == null)
                issue.IssueLabels.Add(issueLabel);
            }
        }

        private void SelectParentStatusInTransitionScheme(int transitionSchemeId, Issue issue)
        {
            var allStatusesChildColumn = this.data.StatusTransitionRepository.Get(
                st => st.TransitionScheme.Id == transitionSchemeId)
                .Select(st => st.ChildStatusId)
                .ToList();
            var rootStatus =
                this.data.StatusTransitionRepository.Get(st => st.TransitionSchemeId == transitionSchemeId
                                                               && !allStatusesChildColumn.Contains(st.ParentStatusId))
                    .Select(st => st.ParentStatus)
                    .FirstOrDefault();

            issue.Status = rootStatus;
        }

        private IEnumerable<IssueViewModel> GetMappedIssuesWithAvailableStatuses(List<Issue> issues)
        {
            var mappedIssues = this.mapper.Map<ICollection<Issue>, ICollection<IssueViewModel>>(issues);
            var availableStatuses = issues.Select(i => GetAvailableStatuses(i.StatusId, i.Project.TransitionSchemeId)).ToList();
            for (int i = 0; i < issues.Count; i++)
            {
                mappedIssues.ElementAt(i).AvailableStatuses = availableStatuses.ElementAt(i);
                mappedIssues.ElementAt(i).Labels =
                  this.mapper.Map<ICollection<IssueLabel>, ICollection<LabelViewModel>>(issues[i].IssueLabels);
            }
            return mappedIssues;
        }

        private IssueViewModel GetMappedIssueWithAvailableStatuses(Issue issue)
        {
            return GetMappedIssuesWithAvailableStatuses(new List<Issue>() { issue }).First();
        }
    }
}
