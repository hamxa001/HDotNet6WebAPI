using HWebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace HWebAPI.IRepository
{
    public interface IRoleRepository
    {
        public Task<ServiceResponse<List<IdentityRole>>> GetAllRoles();
    }
}
