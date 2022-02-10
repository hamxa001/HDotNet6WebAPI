using HWebAPI.Models;

namespace HWebAPI.IRepository
{
    public interface ILoginRepository
    {
        public Task<ServiceResponse<string>> Login(UserLogin login);
    }
}
