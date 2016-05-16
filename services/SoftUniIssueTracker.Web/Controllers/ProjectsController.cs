using System.Web.Http;
using Microsoft.AspNet.Identity;
using SIT.Web.BindingModels.Project;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Controllers
{
    [RoutePrefix("projects")]
    public class ProjectsController : BaseController
    {
        private IProjectsService projectsService;
        private IIssuesService issuesService;

        public ProjectsController(IProjectsService projectsService, IIssuesService issuesService)
        {
            this.projectsService = projectsService;
            this.issuesService = issuesService;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(ProjectBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            var project = this.projectsService.Add(userId, model);
            return CreatedAtRoute("GetProjectById", new { id = project.Id }, project);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Edit(int id, ProjectEditBindingModel model)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var project = this.projectsService.Edit(userId, id, model);
            return CreatedAtRoute("GetProjectById", new { id = project.Id }, project);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var projects = this.projectsService.Get();
            return Ok(projects);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string filter)
        {
            var projects = this.projectsService.Get(pageSize, pageNumber, filter);
            return Ok(projects);
        }

        //[HttpGet]
        //[Route("")]
        //public IHttpActionResult GetByLabel(int pageSize, int pageNumber, string labelName)
        //{
        //    var projects = this.projectsService.GetByLabel(pageSize, pageNumber, labelName);
        //    return Ok(projects);
        //}

        [HttpGet]
        [Route("{id}", Name = "GetProjectById")]
        public IHttpActionResult GetById(int id)
        {
            var project = this.projectsService.GetById(id);
            return Ok(project);
        }

        [HttpGet]
        [Route("{id}/issues")]
        public IHttpActionResult GetIssues(int id)
        {
            var issues = issuesService.GetProjectIssues(id);
            return Ok(issues);
        }
    }
}
