using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;
using SIT.Web.BindingModels;
using SIT.Web.BindingModels.Project;
using SIT.Web.ViewModels.Project;

namespace SIT.Web.Services.Interfaces
{
    public interface IProjectsService
    {
        ProjectViewModel Add(string userId, ProjectBindingModel model);
        ProjectViewModel Edit(string userId, int id, ProjectEditBindingModel model);
        IEnumerable<ProjectViewModel> Get();
        ProjectWithPagesViewModel Get(int pageSize, int pageNumber, string filter);
        ProjectWithPagesViewModel GetByLabel(int pageSize, int pageNumber, string labelName);
        ProjectViewModel GetById(int id);
    }
}
