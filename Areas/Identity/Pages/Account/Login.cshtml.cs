// Sprint 1 ready (done)
#nullable disable

using ETMP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETMP.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        // Inject Dependency of Identity Framework and Logger
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ETMPUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        // Data-binding with cshtml
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        // Able to return to the previously working page
        public string ReturnUrl { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // Enabled password failures to trigger account lockout (Definition of Done)
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Redirect("~/Index");
                }

                // User account will be locked out account after multiple failed login 
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    // Server-side validation error
                    ModelState.AddModelError(string.Empty, "Wrong password or email, please try again");
                    return Page();
                }
            }
            return Page();
        }
    }
}
