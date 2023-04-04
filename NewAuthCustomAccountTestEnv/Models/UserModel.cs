using Microsoft.EntityFrameworkCore;

namespace NewAuthCustomAccountTestEnv.Models
{
    [PrimaryKey(nameof(Id))]
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; } = "MT";
        public string Username { get; set; } = "MT";
        public string Email { get; set; } = "MT";
        public int Coins { get; set; }
        public bool IsAdmin { get; set; }
        public int Failedloginattempts { get; set; }

        public UserModel(string id, string name, string username, string email, int coins, bool isAdmin, int failedloginattempts)
        {
            Id = id;
            Name = name;
            Username = username;
            Email = email;
            Coins = coins;
            IsAdmin = isAdmin;
            Failedloginattempts = failedloginattempts;
        }
    }
}