using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Web.ViewModels.User;

namespace SIT.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserViewModel Author { get; set; }
    }
}
