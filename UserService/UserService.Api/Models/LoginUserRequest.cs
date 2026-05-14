namespace UserService.Api.Models
{
    public class LoginUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
