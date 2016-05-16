using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;
using SIT.Web.BindingModels;
using SIT.Web.BindingModels.Comment;
using SIT.Web.BindingModels.Issue;
using SIT.Web.ViewModels.Comment;
using SIT.Web.ViewModels.Issue;
using SIT.Web.ViewModels.Project;
using SIT.Web.ViewModels.Status;

namespace SIT.Web.Services.Interfaces
{
    public interface IIssuesService
    {
        IssueViewModel Add(string authorId, IssueBindingModel model);
        IssueViewModel Edit(string userId, int id, IssueEditBindingModel model);
        IssueWithPagesViewModel Get(int pageSize, int pageNumber, string filter);
        IssueWithPagesViewModel GetUserIssues(string userId, int pageSize, int pageNumber, string orderBy);
        IEnumerable<IssueViewModel> GetProjectIssues(int projectId);
        IssueViewModel GetById(int id);
        IEnumerable<StatusViewModel> ChangeStatus(string userId, int issueId, int statusId);
        IEnumerable<CommentViewModel> AddComment(int issueId, string authorId, CommentBindingModel model);
        IEnumerable<CommentViewModel> GetIssueComments(int id);
    }
}
