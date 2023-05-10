using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace ETMP.Pages
{
    public class SupportTicketModel : PageModel
    {
        private string? _name;
        private string? _email;
        private string? _about;
        private string? _description;
        public Notification notification { get; set; }
        private readonly ApplicationDbContext _context;
        public SupportTicketModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
                notification = new Notification();
                notification.NotificationHeader = "Training Purchased!";
                notification.NotificationBody = "Training(s) had been purchased";
                notification.IsRead = false;
                notification.NotificationDate = DateTime.Now;
                _context.Notification.Add(notification);
                return Page();
            }
            else
            {
                return RedirectToPage("UserListOfTraining");
            }
        }
    }
}
