using System.Collections.Generic;
using SIT.Web.ViewModels.Label;

namespace SIT.Web.Services.Interfaces
{
    public interface ILabelsService
    {
        IEnumerable<LabelViewModel> GetLabels(string filter);
    }
}