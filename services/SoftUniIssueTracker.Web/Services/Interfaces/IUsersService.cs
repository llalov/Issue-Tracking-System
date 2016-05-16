using System.Collections.Generic;
using SIT.Web.ViewModels.User;

namespace SIT.Web.Services.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetUsers();
        IEnumerable<UserViewModel> GetByFilter(string filter);
        void MakeAdmin(string userId, string userToMakeAdminId);
        UserViewModel GetUser(string id);
    }
}