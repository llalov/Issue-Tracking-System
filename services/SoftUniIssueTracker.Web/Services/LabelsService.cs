using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.Services.Interfaces;
using SIT.Web.ViewModels.Label;

namespace SIT.Web.Services
{
    public class LabelsService : BaseService, ILabelsService
    {
        public LabelsService(ISoftUniIssueTrackerData data, IMapper mapper) : base(data, mapper)
        {
        }
       
        public IEnumerable<LabelViewModel> GetLabels(string filter)
        {
            var labelsQuery = this.data.LabelRepository.Get();
            if (filter != null)
            {
                labelsQuery = labelsQuery.Where(l => l.Name.StartsWith(filter));
            }

            var labels = labelsQuery.ToList();
            return mapper.Map<IEnumerable<Label>, IEnumerable<LabelViewModel>>(labels);
        } 
    }
}