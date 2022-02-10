using HWebAPI.DTOs.UserDTOs;
using HWebAPI.Models;

namespace HWebAPI.IRepository
{
    public interface IUserRepository
    {
        public Task<ServiceResponse<List<UserModel>>> GetAllUsers();
        public Task<ServiceResponse<UserModel>> GetUserByID(string UserID);
        public Task<ServiceResponse<UserModel>> AddUser(AddUserDto Users);
        public Task<ServiceResponse<UserModel>> UpdateUser(string UserID, UserModel Users);
        public Task<ServiceResponse<bool>> DeleteUser(string UserID);
    }
}
