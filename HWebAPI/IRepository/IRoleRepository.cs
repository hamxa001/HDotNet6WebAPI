using HWebAPI.DTOs.RolesDTOs;
using HWebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace HWebAPI.IRepository
{
    public interface IRoleRepository
    {
        public Task<ServiceResponse<List<IdentityRole>>> GetAllRoles();
        public Task<ServiceResponse<List<IdentityRole>>> AddRoles(IEnumerable<AddRolesDto> roles);
        public Task<ServiceResponse<int>> DeleteRole(string rolesId);
    }
}
