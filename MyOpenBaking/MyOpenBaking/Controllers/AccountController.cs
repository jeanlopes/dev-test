using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOpenBanking.Application.Services;
using MyOpenBanking.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyOpenBaking.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly UserService _service;
        public AccountController(UserService service) => _service = service;
        
       
        [HttpGet]
        public JsonResult GetUsers() => Json(_service.GetUsers());

        [AllowAnonymous]
        [HttpPost("signin")]
        public ActionResult Signin([FromBody] User user)
        {
            var token = _service.Authenticate(user.UserName, user.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { accessToken=token, user });
        }

        // POST api/<AccountController>
        [AllowAnonymous]
        [HttpPost("signup")]
        public JsonResult Signup([FromBody] User user)
        {
            _service.Create(user);
            return Json(user);
        }


        [HttpGet("{id:length(24)}")]
        public JsonResult GetUser(string id) => Json(_service.GetUser(id));


        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}