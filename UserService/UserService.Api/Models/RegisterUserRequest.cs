namespace UserService.Api.Models
{
    public class RegisterUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
