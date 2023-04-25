// Sprint 1 ready (done)
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ETMP.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly IUserStore<ETMPUser> _userStore;
        private readonly IUserEmailStore<ETMPUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly Services.IMailService _mailService;

        public RegisterModel(
            UserManager<ETMPUser> userManager,
            IUserStore<ETMPUser> userStore,
            SignInManager<ETMPUser> signInManager,
            ILogger<RegisterModel> logger,
            Services.IMailService mailService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _mailService = mailService;
        }

        // Store last active page
        public string ReturnUrl { get; set; }

        // Data-binding with cshtml
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Confirm Email")]
            // Validation for email confirmation
            [Compare("Email", ErrorMessage = "The email do not match.")]
            public string ConfirmEmail { get; set; }

            [Required]
            // Validation for the password length (including empty field)
            [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            // Validation for password confirmation
            [Compare("Password", ErrorMessage = "The password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // All registered new account have basic privilage (non-admin)
                    await _userManager.AddToRoleAsync(user, "Member");
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    var request = new MailRequest(Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", null);
                    await _mailService.SendEmailAsync(request);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    // Server-side validation
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }

        private ETMPUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ETMPUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ETMPUser)}'. ");
            }
        }

        // Identity service email storage object
        private IUserEmailStore<ETMPUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ETMPUser>)_userStore;
        }
    }
}
