using HWebAPI.DTOs.UserDTOs;
using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository UserRepo)
        {
            _userRepo = UserRepo;
        }

        [HttpGet("allusers")]
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> Get()
        {
            var result = await _userRepo.GetAllUsers();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<ServiceResponse<UserModel>>> Getuser(string GuidId)
        {
            var result = await _userRepo.GetUserByID(GuidId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("adduser")]
        public async Task<ActionResult<ServiceResponse<UserModel>>> Adduser(AddUserDto Users)
        {
            var result = await _userRepo.AddUser(Users);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("updateuser")]
        public async Task<ActionResult<ServiceResponse<UserModel>>> updateuser(string UserID, UserModel Users)
        {
            var result = await _userRepo.UpdateUser(UserID, Users);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("deleteuser")]
        public async Task<ActionResult<bool>> deleteuser(string UserID)
        {
            var result = await _userRepo.DeleteUser(UserID);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
