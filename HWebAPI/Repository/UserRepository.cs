using AutoMapper;
using HWebAPI.DTOs.UserDTOs;
using HWebAPI.Helpers;
using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<UserModel>>> GetAllUsers()
        {
            var ServiceResponse = new ServiceResponse<List<UserModel>>();
            try
            {
                var dbUsers = await _context.Users.ToListAsync();
                ServiceResponse.Data = dbUsers;
                ServiceResponse.Success = true;
                ServiceResponse.Message = dbUsers.Count > 0 ? "All Users Fetched!" : "No user exists!";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        public async Task<ServiceResponse<UserModel>> GetUserByID(string UserID)
        {
            var ServiceResponse = new ServiceResponse<UserModel>();
            try
            {
                var users = await _context.Users.SingleAsync(x => x.Id == UserID);
                ServiceResponse.Data = users;
                ServiceResponse.Success = true;
                ServiceResponse.Message = "User successfully fetched!";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        public async Task<ServiceResponse<UserModel>> AddUser(AddUserDto user)
        {
            var ServiceResponse = new ServiceResponse<UserModel>();
            UserModel Users = _mapper.Map<UserModel>(user);
            try
            {
                if (await UserExist(Users.UserName))
                {
                    ServiceResponse.Success = false;
                    ServiceResponse.Message = "User Already Exist";
                    return ServiceResponse;
                }
                CreatePasswordHash(Users.PasswordHash, out byte[] PasswordMade, out byte[] PasswordSalt);

                Users.Id = CommonCode.GuidID();
                Users.SecurityStamp = CommonCode.GuidID();
                Users.ConcurrencyStamp = CommonCode.GuidID();
                Users.PasswordHash = Convert.ToBase64String(PasswordMade);
                Users.PasswordSalt = Convert.ToBase64String(PasswordSalt);
                //add user using following code
                var AddUser = await _context.Users.AddAsync(Users);
                await _context.SaveChangesAsync();
                // now add user role using the following code
                var AssignRole = await _context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = Users.Id, RoleId = user.Userroleid });
                await _context.SaveChangesAsync();
                // ON success complete request using following code
                ServiceResponse.Data = await _context.Users.SingleAsync(x => x.Id == Users.Id);
                ServiceResponse.Success = true;
                ServiceResponse.Message = "User successfully added!";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        public async Task<ServiceResponse<UserModel>> UpdateUser(string UserID, UserModel UpdateUsers)
        {
            var ServiceResponse = new ServiceResponse<UserModel>();
            try
            {
                var Users = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserID);
                Users.UserName = UpdateUsers.UserName;
                Users.AccessFailedCount = UpdateUsers.AccessFailedCount;
                Users.ConcurrencyStamp = UpdateUsers.ConcurrencyStamp;
                Users.Email = UpdateUsers.Email;
                Users.EmailConfirmed = UpdateUsers.EmailConfirmed;
                Users.LockoutEnabled = UpdateUsers.LockoutEnabled;
                Users.LockoutEnd = UpdateUsers.LockoutEnd;
                Users.NormalizedEmail = UpdateUsers.NormalizedEmail;
                Users.NormalizedUserName = UpdateUsers.NormalizedUserName;
                Users.PhoneNumber = UpdateUsers.PhoneNumber;
                Users.PhoneNumberConfirmed = UpdateUsers.PhoneNumberConfirmed;
                Users.SecurityStamp = UpdateUsers.SecurityStamp;
                Users.TwoFactorEnabled = UpdateUsers.TwoFactorEnabled;

                await _context.SaveChangesAsync();

                ServiceResponse.Data = await _context.Users.SingleAsync(x => x.Id == UserID);
                ServiceResponse.Success = true;
                ServiceResponse.Message = "User successfully Updated!";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        public async Task<ServiceResponse<bool>> DeleteUser(string UserID)
        {
            var ServiceResponse = new ServiceResponse<bool>();
            try
            {
                var DeleteUser = await _context.Users.SingleAsync(x => x.Id == UserID);
                _context.Users.Remove(DeleteUser);
                await _context.SaveChangesAsync();

                ServiceResponse.Data = true;
                ServiceResponse.Success = true;
                ServiceResponse.Message = "User successfully Deleted!";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }


        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }

        private async Task<bool> UserExist(string Username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName.ToLower().Equals(Username.ToLower())))
            {
                return true;
            }
            return false;
        }
    }
}
