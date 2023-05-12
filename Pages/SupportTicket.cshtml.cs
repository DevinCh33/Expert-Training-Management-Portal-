using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;

namespace ETMP.Pages
{
    public class SupportTicketModel : PageModel
    {
        private string? _name;
        private string? _email;
        private string? _about;
        private string? _description;
        private readonly Services.IMailService _mailService;
        public Notification notification { get; set; }
        private readonly ApplicationDbContext _context;
        public SupportTicketModel(Services.IMailService mailService, ApplicationDbContext context )
        {
            _mailService = mailService;
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                string subject = "Support Ticket On " + _about;
                string body = "<strong>Name of Sender: </strong>" + _name + "<br/><br/><strong>Email of Sender: </strong>" + _email + "<br/><br/><strong>Description of Issue: </strong>" + Description;

                string toEmail = "swe20001projectticket@gmail.com";
                //string toEmail = "emtpsdnbhd@gmail.com";

                var request = new MailRequest(toEmail, subject, body, null);

                //Sends Email
                await _mailService.SendEmailAsync(request);

                //notification
                
                notification = new Notification();
                notification.NotificationHeader = "Support ticket has been sent!";
                notification.NotificationBody = "Your support ticket regarding " + _about + " has been sent!";
                notification.NotificationDate = DateTime.Now;
                _context.Notification.Add(notification);
                _context.SaveChanges();
               
                //notifcation ended


                return RedirectToPage("SupportTicket");
            }
        }
    }
}
