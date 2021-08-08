using Microsoft.AspNetCore.Mvc;

namespace MyOpenBaking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {

        [HttpGet("all")]
        public string All()
        {
            return "Hello";
        }
    }
}
