using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NewAuthCustomAccountTestEnv.Data
{
    [PrimaryKey(nameof(UserName))]
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public int Coins { get; set; } = 3;

        public bool IsAdmin { get; set; } = false;
    }
}