using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIT.Web.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool isAdmin { get; set; }
    }
}
