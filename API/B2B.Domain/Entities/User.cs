namespace Domain.B2B.Domain.Entities
{       

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Role { get; set; }

        public User(string name, string email, int id, string password, string role)
        {
            Id = id;
            Name = name;
            Email = email;            
            Password = password;
            Role = role;
        }
    }
}

