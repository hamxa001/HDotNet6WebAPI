using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HWebAPI.Controllers
{
    //[EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roles;
        public RolesController(IRoleRepository roles)
        {
            _roles = roles;
        }
        [HttpGet("allroles")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<IdentityRole>>>> Get()
        {
            var result = await _roles.GetAllRoles();
            if (result.Success)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }
    }
}
