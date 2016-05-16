using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;

namespace SIT.Web.BindingModels.Comment
{
    public class CommentBindingModel
    {
        [Required]
        public string Text { get; set; }
    }
}