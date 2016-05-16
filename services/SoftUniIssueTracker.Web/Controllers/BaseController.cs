using System.Web.Http;

namespace SIT.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : ApiController
    {
        //protected string userId = "ae778bf8-bbb6-4e6f-a16e-78c55c7d0eeb";
    }
}
