using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NewAuthCustomAccountTestEnv.Data
{
    [PrimaryKey(nameof(UserName))]
    public class ApplicationUser : IdentityUser
    {
        #region Properties

        public int Coins { get; set; } = 3;
        public bool IsAdmin { get; set; } = false;
        public long LBPrivacy { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

        #endregion Properties
    }
}