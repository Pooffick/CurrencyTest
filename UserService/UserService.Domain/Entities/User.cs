namespace UserService.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        private User() { }

        public User(string name, string passwordHash)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            PasswordHash = passwordHash;
        }
    }
}
