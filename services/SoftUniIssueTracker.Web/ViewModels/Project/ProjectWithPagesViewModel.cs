using System.Collections.Generic;
using SIT.Web.ViewModels.Project;

namespace SIT.Web.ViewModels.Project
{
    public class ProjectWithPagesViewModel
    {
        public double TotalPages { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }
        public int TotalCount { get; set; }
    }
}
