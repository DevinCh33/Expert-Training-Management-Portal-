using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ETMP.Pages
{
    public class EditUserProfileModel : PageModel
    {
        private readonly ILogger<EditUserProfileModel> _logger;

        public EditUserProfileModel(ILogger<EditUserProfileModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Sex { get; set; }

        public void OnGet()
        {
            // Fetch user details from database or any other source and set the values to the properties
            Name = "John Doe"; // Example name
            Sex = "Male"; // Example sex
        }

        public IActionResult OnPost()
        {
            // Update user details in database or any other source using the values from the properties
            // Name and Sex properties will contain the updated values submitted from the form
            // You can add validation and error handling here

            // Redirect to a success page or any other desired action
            return RedirectToPage("/Success");
        }
    }
}
