using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIT.Web.ViewModels.Issue
{
    public class IssueWithPagesViewModel
    {
        public double TotalPages { get; set; }
        public IEnumerable<IssueViewModel> Issues { get; set; }
        public int TotalCount { get; set; }
    }
}
