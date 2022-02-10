using Microsoft.AspNetCore.Identity;

namespace HWebAPI.Models
{
    public class UserModel : IdentityUser
    {
        public string PasswordSalt { get; set; } = string.Empty;
    }
}
