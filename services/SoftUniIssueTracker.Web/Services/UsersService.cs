using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.Services.Interfaces;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.User;

namespace SIT.Web.Services
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(ISoftUniIssueTrackerData data, IMapper mapper) : base(data, mapper)
        {
        }
       
        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = this.data.UserRepository.GetAll().ToList();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
        }

        public IEnumerable<UserViewModel> GetByFilter(string filter)
        {
            var usersQuery = this.data.UserRepository.GetAll();
            if (filter != null)
            {
                usersQuery = usersQuery.Where(filter);
            }

            var users = usersQuery.ToList();
            return this.mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
        } 

        public UserViewModel GetUser(string id)
        {
            var user = GetById(id);
            return this.mapper.Map<User, UserViewModel>(user);
        }

        private User GetById(string id)
        {
            return this.data.UserRepository.GetById(id);
        }

        public void MakeAdmin(string userId, string userToMakeAdminId)
        {
            var user = GetById(userId);
            if (!user.isAdmin)
            {
                throw new InvalidOperationException(Constants.NotAdmin);
            }

            var userToMakeAdmin = GetById(userToMakeAdminId);
            if (userToMakeAdmin == null)
            {
                throw new ArgumentException(Constants.UnexistingUserErrorMessage);
            }

            if (userToMakeAdmin.isAdmin)
            {
                throw new InvalidOperationException(Constants.AlreadyAdmin);
            }

            userToMakeAdmin.isAdmin = true;
            this.data.Save();
        }
    }
}