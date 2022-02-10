using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HWebAPI.Repository
{
    public class RolesRepository : IRoleRepository
    {
        private readonly DBContext _context;
        public RolesRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<IdentityRole>>> GetAllRoles()
        {
            var ServiceResponse = new ServiceResponse<List<IdentityRole>>();
            try
            {
                var dbRoles = await _context.Roles.ToListAsync();
                ServiceResponse.Data = dbRoles;
                ServiceResponse.Success = true;
                ServiceResponse.Message = dbRoles.Count > 0 ? "All Roles Successfully Fetched!" : "No Roles Exist!";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
    }
}
