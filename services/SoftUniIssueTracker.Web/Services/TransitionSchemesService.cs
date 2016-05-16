using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Services
{
    public class TransitionSchemesService : BaseService, ITransitionSchemeService
    {
        public TransitionSchemesService(ISoftUniIssueTrackerData data, IMapper mapper) : base(data, mapper)
        {
        }

        public TransitionScheme AddDefaultTransitionScheme()
        {
            var transitionScheme = new TransitionScheme()
            {
                Name = Constants.DefaultSchemeName,
                IsDefault = true
            };

            var openStatus = this.data.StatusRepository.Get(s => s.Name == DefaultTransitionSchemeStatuses.Open.ToString()).FirstOrDefault();
            var closedStatus = this.data.StatusRepository.Get(s => s.Name == DefaultTransitionSchemeStatuses.Closed.ToString()).FirstOrDefault();
            var inProgressStatus = this.data.StatusRepository.Get(s => s.Name == DefaultTransitionSchemeStatuses.InProgress.ToString()).FirstOrDefault();
            var stoppedProgressStatus = this.data.StatusRepository.Get(s => s.Name == DefaultTransitionSchemeStatuses.StoppedProgress.ToString()).FirstOrDefault();
            if (openStatus == null)
            {
                openStatus = new Status()
                {
                    Name = DefaultTransitionSchemeStatuses.Open.ToString()
                };
                this.data.StatusRepository.Insert(openStatus);
            }

            if (closedStatus == null)
            {
                closedStatus = new Status()
                {
                    Name = DefaultTransitionSchemeStatuses.Closed.ToString()
                };
                this.data.StatusRepository.Insert(closedStatus);
            }

            if (inProgressStatus == null)
            {
                inProgressStatus = new Status()
                {
                    Name = DefaultTransitionSchemeStatuses.InProgress.ToString()
                };
                this.data.StatusRepository.Insert(inProgressStatus);
            }

            if (stoppedProgressStatus == null)
            {
                stoppedProgressStatus = new Status()
                {
                    Name = DefaultTransitionSchemeStatuses.StoppedProgress.ToString()
                };
                this.data.StatusRepository.Insert(stoppedProgressStatus);
            }

            this.data.StatusTransitionRepository.Insert(new StatusTransition()
            {
                ParentStatus = openStatus,
                ChildStatus = closedStatus,
                TransitionScheme = transitionScheme
            });

            this.data.StatusTransitionRepository.Insert(new StatusTransition()
            {
                ParentStatus = openStatus,
                ChildStatus = inProgressStatus,
                TransitionScheme = transitionScheme
            });

            this.data.StatusTransitionRepository.Insert(new StatusTransition()
            {
                ParentStatus = inProgressStatus,
                ChildStatus = stoppedProgressStatus,
                TransitionScheme = transitionScheme
            });

            this.data.StatusTransitionRepository.Insert(new StatusTransition()
            {
                ParentStatus = inProgressStatus,
                ChildStatus = closedStatus,
                TransitionScheme = transitionScheme
            });

            this.data.StatusTransitionRepository.Insert(new StatusTransition()
            {
                ParentStatus = stoppedProgressStatus,
                ChildStatus = inProgressStatus,
                TransitionScheme = transitionScheme
            });

            this.data.StatusTransitionRepository.Insert(new StatusTransition()
            {
                ParentStatus = stoppedProgressStatus,
                ChildStatus = closedStatus,
                TransitionScheme = transitionScheme
            });

            this.data.TransitionSchemeRepository.Insert(transitionScheme);
            return transitionScheme;
        }
    }
}
