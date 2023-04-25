// Sprint 1 ready (done)
#nullable disable

using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace ETMP.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        // Inject Dependency of Identity Framework and Email Service
        private readonly UserManager<ETMPUser> _userManager;
        private readonly Services.IMailService _mailService;

        public ForgotPasswordModel(UserManager<ETMPUser> userManager, Services.IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        // Data-binding with cshtml
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        // Called when cshtml is submitted (button pressed)
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Find user through searching email
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Security purpose - dont show bad-actor if the account exist or not.
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // Generate a URL for user to click on in their email for resetting password
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                // Send the email
                var request = new MailRequest(Input.Email, "Reset Password", $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", null);
                await _mailService.SendEmailAsync(request);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }
            return Page();
        }
    }
}
