// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewAuthCustomAccountTestEnv.Data;

namespace NewAuthCustomAccountTestEnv.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        #region Fields

        private readonly ILogger<PersonalDataModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion Fields

        #region Public Constructors

        public PersonalDataModel(
            UserManager<ApplicationUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        #endregion Public Methods
    }
}