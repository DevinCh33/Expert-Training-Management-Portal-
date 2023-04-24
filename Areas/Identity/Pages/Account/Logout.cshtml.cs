// Sprint 1 ready (done)
#nullable disable

using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        // Inject Dependency of Identity Framework and Logger
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ETMPUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage();
        }
    }
}
