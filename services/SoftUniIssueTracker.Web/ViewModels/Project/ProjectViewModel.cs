using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.Priority;
using SIT.Web.ViewModels.User;

namespace SIT.Web.ViewModels.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProjectKey { get; set; }

        public string Description { get; set; }

        public UserViewModel Lead { get; set; }

        public int TransitionSchemeId { get; set; }

        public ICollection<LabelViewModel> Labels { get; set; }

        public ICollection<PriorityViewModel> Priorities { get; set; }
    }
}
