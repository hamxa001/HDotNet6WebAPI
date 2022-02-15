using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HWebAPI.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;

        public LoginRepository(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(UserLogin Credentials)
        {
            var ServiceResponse = new ServiceResponse<string>();
            try
            {
                var User = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == Credentials.username.ToLower());

                if (User == null)
                {
                    ServiceResponse.Success = false;
                    ServiceResponse.Message = "User not found.";
                }
                else
                if (!VerifyPasswordHash(Credentials.password, Convert.FromBase64String(User.PasswordHash), Convert.FromBase64String(User.PasswordSalt)))
                {
                    ServiceResponse.Success = false;
                    ServiceResponse.Message = "Username Or Password Is Incorrect";
                }
                else
                {
                    ServiceResponse.Data = await CreateToken(User);
                    ServiceResponse.Success = true;
                    ServiceResponse.Message = "Login is Successfull!";
                }
            }
            catch (Exception ex)
            {
                ServiceResponse.Data = string.Empty;
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private async Task<string> CreateToken(UserModel users)
        {
            var UserRoles = await _context.UserRoles.FirstAsync(x=>x.UserId == users.Id);
            var Role = await _context.Roles.FirstAsync(x=>x.Id == UserRoles.RoleId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, users.Id),
                new Claim(ClaimTypes.Name, users.UserName),
                new Claim(ClaimTypes.Email, users.Email),
                new Claim(ClaimTypes.Role, Role.Name.ToLower())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("jwt:Key").Value
                ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokendescr = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokendescr);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenHandler.WriteToken(token);
        }
    }
}
