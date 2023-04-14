using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ETMP.Areas.Identity.Pages.Account.Manage
{
    public class OrganisationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;



public string OrganisationName { get; set; }

public string OrganisationMailingAddress { get; set; }

public string TrainingTeamName { get; set; }
        public OrganisationModel(
       UserManager<IdentityUser> userManager,
       ILogger<PersonalDataModel> logger)
   {
       _userManager = userManager;
       _logger = logger;
   }

   public async Task<IActionResult> OnGet()
   {
       var user = await _userManager.GetUserAsync(User);
       if (user == null)
       {
           return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
       }

       return Page();
   }
}
}
