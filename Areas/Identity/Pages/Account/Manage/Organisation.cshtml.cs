#nullable disable

using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETMP.Areas.Identity.Pages.Account.Manage
{
    public class OrganisationModel : PageModel
    {
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;

        public OrganisationModel(
            UserManager<ETMPUser> userManager,
            SignInManager<ETMPUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "Organisation Name")]
            public string OrganisationName { get; set; }

            [Display(Name = "Organisation Mailing Address")]
            [DataType(DataType.Text)]
            public string OrganisationMailingAddress { get; set; }

            [Display(Name = "Training Team Name")]
            [DataType(DataType.Text)]
            public string TrainingTeamName { get; set; }
        }

        private async Task LoadAsync(ETMPUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;
            Input = new InputModel
            {
                OrganisationName = user.OrganisationName,
                OrganisationMailingAddress = user.OrganisationMailingAddress,
                TrainingTeamName = user.TrainingTeamName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.OrganisationName != user.OrganisationName)
            {
                user.OrganisationName = Input.OrganisationName;
            }

            if (Input.OrganisationMailingAddress != user.OrganisationMailingAddress)
            {
                user.OrganisationMailingAddress = Input.OrganisationMailingAddress;
            }

            if (Input.TrainingTeamName != user.TrainingTeamName)
            {
                user.TrainingTeamName = Input.TrainingTeamName;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
