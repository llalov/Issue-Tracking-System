using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using SIT.Web.BindingModels.Comment;
using SIT.Web.BindingModels.Issue;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Controllers
{
    [RoutePrefix("issues")]
    public class IssuesController : BaseController
    {
        private IIssuesService issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            this.issuesService = issuesService;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(IssueBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            var issue = this.issuesService.Add(userId, model);
            return CreatedAtRoute("GetIssueById", new { id = issue.Id }, issue);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Edit(int id, IssueEditBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            var issue = this.issuesService.Edit(userId, id, model);
            return Ok(issue);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string filter)
        {
            var issues = issuesService.Get(pageSize, pageNumber, filter);
            return Ok(issues);
        }

        [HttpGet]
        [Route("me")]
        public IHttpActionResult GetUserIssues(int pageSize, int pageNumber, string orderBy)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var issues = issuesService.GetUserIssues(userId, pageSize, pageNumber, orderBy);
            return Ok(issues);
        }

        [HttpGet]
        [Route("{id}", Name = "GetIssueById")]
        public IHttpActionResult GetById(int id)
        {
            var issue = issuesService.GetById(id);
            return Ok(issue);
        }

        [HttpPut]
        [Route("{id}/changestatus")]
        public IHttpActionResult ChangeStatus(int id, int statusId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            var availableStatuses = issuesService.ChangeStatus(userId, id, statusId);
            return Ok(availableStatuses);
        }

        [HttpPost]
        [Route("{id}/comments")]
        public IHttpActionResult AddComment(int id, CommentBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            var allIssueComments = issuesService.AddComment(id, userId, model);
            return CreatedAtRoute("GetIssueComments", new { id = id }, allIssueComments);
        }

        [HttpGet]
        [Route("{id}/comments", Name = "GetIssueComments")]
        public IHttpActionResult GetIssueComments(int id)
        { 
            var comments = issuesService.GetIssueComments(id);
            return Ok(comments);
        }
    }
}