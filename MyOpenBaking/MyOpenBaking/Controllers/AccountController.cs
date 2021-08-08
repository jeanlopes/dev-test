using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOpenBanking.Application.Services.Interface;
using MyOpenBanking.Domain.Entities;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyOpenBaking.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly IUserService _service;
        public AccountController(IUserService service) => _service = service;
        
       
        [HttpGet]
        public async Task<JsonResult> GetUsers() => Json(await _service.GetUsers());

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<ActionResult> Signin([FromBody] User user)
        {
            var token = await _service.Authenticate(user.UserName, user.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { accessToken=token, user });
        }

        // POST api/<AccountController>
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<JsonResult> Signup([FromBody] User user)
        {
            await _service.Create(user);
            return Json(user);
        }


        [HttpGet("{id:length(24)}")]
        public async Task<JsonResult> GetUser(string id) => Json(await _service.GetUser(id));


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