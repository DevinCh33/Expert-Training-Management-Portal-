using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Data;
using System.Text;
using System.Text.Encodings.Web;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin")]
    public class ManageUserAccountModel : PageModel
    {
        private List<ETMPUser> _userList;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ManageUserAccountModel> _logger;
        private readonly Services.IMailService _mailService;

        public ManageUserAccountModel(ApplicationDbContext context, UserManager<ETMPUser> userManager, ILogger<ManageUserAccountModel> logger, Services.IMailService mailService)
        {
            _context = context;
            _userList = new List<ETMPUser>();
            _userManager = userManager;
            _logger = logger;
            _mailService = mailService;
        }

        public List<ETMPUser> UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            _userList = _context.Users.ToList();
        }

        public IActionResult OnGetFilterOptions(string filterType)
        {
            _userList = _context.Users.ToList();
            var filterOptions = new List<SelectListItem>();

            switch (filterType)
            {
                case "NoFilter":
                    filterOptions.Add(new SelectListItem { Value = "-", Text = "-" });
                    break;
                case "Gender":
                    var genders = _userList.Select(u => u.Gender).Distinct();
                    genders = genders.Where(i => i != null).ToList();
                    filterOptions.AddRange(genders.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                case "OrganisationName":
                    var organisations = _userList.Select(u => u.OrganisationName).Distinct();
                    organisations = organisations.Where(i => i != null).ToList();
                    filterOptions.AddRange(organisations.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                case "TrainingTeamName":
                    var teams = _userList.Select(u => u.TrainingTeamName).Distinct();
                    teams = teams.Where(i => i != null).ToList();
                    filterOptions.AddRange(teams.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                default:
                    break;
            }
            return new JsonResult(filterOptions);
        }

        public IActionResult OnGetFilterTable(string filterColumn, string filterValue)
        {
            _userList = _context.Users.ToList();
            var filteredUsers = _userList.Where(u => u.GetType().GetProperty(filterColumn)?.GetValue(u)?.ToString() == filterValue).ToList();
            _logger.LogInformation("Filter List Request Responded, Return Size: " + filteredUsers.Count);
            foreach(var user in filteredUsers)
            {
                _logger.LogInformation("Email: " + user.Email);
                _logger.LogInformation("First Name: " + user.FirstName);
                _logger.LogInformation("Last Name: " + user.LastName);
                _logger.LogInformation("Gender: " + user.Gender);
                _logger.LogInformation("Organisation: " + user.OrganisationName);
                _logger.LogInformation("Team: " + user.TrainingTeamName);
            }
            if (filterColumn == "NoFilter")
            {
                filteredUsers = _userList;
            }
            return new JsonResult(filteredUsers);  
        }

        public async Task<IActionResult> OnPostDeleteAccountAsync(string userId)
        {
            System.Diagnostics.Debug.WriteLine("!!!!Delete Account: ");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                // Handle the delete error
                return BadRequest();
            }

            // Delete successful
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendResetPasswordAsync(string userId)
        {
            System.Diagnostics.Debug.WriteLine("!!!!Reset Password: ");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme);

            // Send the email
            var request = new MailRequest(user.Email, "Reset Password", $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", null);
            await _mailService.SendEmailAsync(request);

            // Password reset email sent successfully
            return Page();
        }
    }
}
