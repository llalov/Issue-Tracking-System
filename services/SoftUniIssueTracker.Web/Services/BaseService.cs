using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SIT.Common;
using SIT.Data.Interfaces;

namespace SIT.Web.Services
{
    public abstract class BaseService
    {
        protected ISoftUniIssueTrackerData data;
        protected IMapper mapper;

        public BaseService(ISoftUniIssueTrackerData data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        //protected void RestrictTo(IEnumerable<UserRoles> roles, string userId)
        //{
        //    var canPass = false;
        //    foreach (var role in roles)
        //    {
        //        if (role == UserRoles.Admin)
        //        {
                    
        //        }
        //        if (role == UserRoles.ProjectLeader)
        //        {
                    
        //        }
        //        if (role == UserRoles.IssueAssignee)
        //        {
                    
        //        }
        //    }
        //}
        //protected bool isAdmin()
        //{
            
        //}

        //protected bool isProjectLeader()
        //{
            
        //}

        //protected bool isIssueAssignee()
        //{
            
        //}
    }
}
