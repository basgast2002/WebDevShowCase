using Microsoft.EntityFrameworkCore;

namespace NewAuthCustomAccountTestEnv.Models
{
    [PrimaryKey(nameof(Id))]
    public class UserModel
    {
        #region Public Constructors

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

        #endregion Public Constructors

        #region Properties

        public int Coins { get; set; }
        public string Email { get; set; } = "MT";
        public int Failedloginattempts { get; set; }
        public string Id { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; } = "MT";
        public string Username { get; set; } = "MT";

        #endregion Properties
    }
}