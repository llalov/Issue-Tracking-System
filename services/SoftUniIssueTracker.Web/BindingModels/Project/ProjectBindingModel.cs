using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;

namespace SIT.Web.BindingModels.Project
{
    public class ProjectBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ProjectKey { get; set; }

        [Required]
        public string LeadId { get; set; }

        public int? TransitionSchemeId { get; set; }

        public List<Label> Labels { get; set; } 

        [Required]
        public List<Priority> Priorities { get; set; } 
    }
}
