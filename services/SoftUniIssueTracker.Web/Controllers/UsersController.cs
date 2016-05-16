using System;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SIT.Models;
using SIT.Web.BindingModels.Project;
using SIT.Web.BindingModels.User;
using SIT.Web.Services;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var users = this.usersService.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetByFilter(string filter)
        {
            var users = this.usersService.GetByFilter(filter);
            return Ok(users);
        }

        [HttpGet]
        [Route("me")]
        public IHttpActionResult GetCurrentUser()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var user = this.usersService.GetUser(userId);
            return Ok(user);
        }

        [HttpPut]
        [Route("makeadmin")]
        public IHttpActionResult MakeAdmin(MakeUserAdminBindingModel model)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            this.usersService.MakeAdmin(userId, model.UserId);
            return Ok();
        }

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("changepass/{userId}")]
        //public IHttpActionResult ChangeAdminPassword(string userId)
        //{
        //    var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        //    var resetToken = manager.GeneratePasswordResetToken(userId);
        //    var passwordChangeResult = manager.ResetPassword(userId, resetToken, "123456");

        //    if (!passwordChangeResult.Succeeded)
        //    {
        //        return BadRequest("Възникна грешка при смяната на паролата на потребител. Моля, опитайте отново по-късно.");
        //    }
        //    return Ok("Смяната е успешна");
        //}
    }
}
