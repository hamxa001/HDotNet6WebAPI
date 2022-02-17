using AutoMapper;
using HWebAPI.DTOs.RolesDTOs;
using HWebAPI.Helpers;
using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HWebAPI.Repository
{
    public class RolesRepository : IRoleRepository
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public RolesRepository(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<ServiceResponse<List<IdentityRole>>> AddRoles(IEnumerable<AddRolesDto> roles)
        {
            var ServiceResponse = new ServiceResponse<List<IdentityRole>>();
            var Roles = _mapper.Map<IEnumerable<IdentityRole>>(roles);
            try
            {
                foreach (var role in Roles)
                {
                    role.Id = CommonCode.GuidID();
                    role.ConcurrencyStamp = CommonCode.GuidID();
                }

                await _context.AddRangeAsync(roles);
                await _context.SaveChangesAsync();

                var listIds = Roles.Select(x => x.Id).ToList();
                                
                ServiceResponse.Data = _context.Roles.Where(x => listIds.Contains(x.Id)).ToList();
                ServiceResponse.Success = true;
                ServiceResponse.Message = "Roles Successfuly inserted!";
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
