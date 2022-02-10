namespace HWebAPI.Models
{
    public class UserRoleModel : Microsoft.AspNetCore.Identity.IdentityUserRole<string>
    {
        public override string UserId { get; set; }
        public override string RoleId { get; set; }
    }
}