using System.Security.Cryptography;
using System.Text;
using UserService.Application.Abstractions;

namespace UserService.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public bool Verify(string password, string hash) => Hash(password) == hash;
    }
}
