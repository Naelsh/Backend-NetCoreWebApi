using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected User GetAuthenticatedUser()
        {
            return (User)HttpContext.Items["User"];
        }

        protected int GetAuthenticatedUserId()
        {
            return ((User)HttpContext.Items["User"]).Id;
        }
    }
}
