using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using NewAuthCustomAccountTestEnv.Data;

namespace NewAuthCustomAccountTestEnv.Areas.Identity.Pages.Account.Manage
{
    public class PrivacyModel : PageModel
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SqliteConnection DatabaseConnection = new("DataSource=AuthDb.db;");

        #endregion Fields

        #region Public Constructors

        public PrivacyModel(UserManager<ApplicationUser> userManager, ILogger<Disable2faModel> logger)
        {
            _userManager = userManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                string errormessage = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound(errormessage);
            }
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                string errormessage = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound(errormessage);
            }
            long currentStatus = user.LBPrivacy;
            if (currentStatus > 0)
            {
                UpdateLeaderboardPrivacy(0);
            }
            else
            {
                UpdateLeaderboardPrivacy(1);
            }
            return Redirect("#");
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<bool> UpdateLeaderboardPrivacy(long value)
        {
            var user = await _userManager.GetUserAsync(User);
            await DatabaseConnection.OpenAsync();
            using (var fmd = DatabaseConnection.CreateCommand())
            {
                fmd.CommandText = @"UPDATE AspNetUsers SET LBPrivacy = $LBPrivacy WHERE id = $id;";
                fmd.Parameters.AddWithValue("$LBPrivacy", value);
                fmd.Parameters.AddWithValue("$id", user.Id);
                await fmd.ExecuteNonQueryAsync();
            }
            await DatabaseConnection.CloseAsync();
            return true;
        }

        #endregion Private Methods
    }
}