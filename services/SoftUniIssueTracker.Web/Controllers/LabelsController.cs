using System.Web.Http;
using Microsoft.AspNet.Identity;
using SIT.Web.BindingModels.Project;
using SIT.Web.Services;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Controllers
{
    [RoutePrefix("labels")]
    public class LabelsController : BaseController
    {
        private ILabelsService labelsService;

        public LabelsController(ILabelsService labelsService)
        {
            this.labelsService = labelsService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetLabels(string filter)
        {
            var labels = this.labelsService.GetLabels(filter);
            return Ok(labels);
        }
    }
}
