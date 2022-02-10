namespace HWebAPI.DTOs.UserDTOs
{
    public class AddUserDto
    {
        public string UserName { get; set; } = String.Empty;
        public string NormalizedUserName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Userroleid { get; set; } = string.Empty;
    }
}
