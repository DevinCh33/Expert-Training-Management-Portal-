using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace ETMP.Pages
{
    public class SupportTicketModel : PageModel
    {
        private string? _name;
        private string? _email;
        private string? _about;
        private string? _description;

        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [BindProperty]
        [Required(ErrorMessage = "This field must not be empty")]
        public string About
        {
            get { return _about; }
            set { _about = value; }
        }


        [BindProperty]
        [Required(ErrorMessage = "This field must not be empty")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("UserListOfTraining");
            }
        }
    }
}
