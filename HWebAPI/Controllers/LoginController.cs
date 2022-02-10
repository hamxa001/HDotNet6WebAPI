using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepo;

        public LoginController(ILoginRepository LoginRepo)
        {
            _loginRepo = LoginRepo;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin login)
        {
            var result = await _loginRepo.Login(login);
            return Ok(result);
        }
    }
}
