using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyOpenBaking.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("all")]
        public string Get()
        {
            return "Hello Admin";
        }
    }
}
