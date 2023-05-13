using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using PuppeteerSharp;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ETMP.Pages
{
    public class PaymentFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private int _toBuyId;
        public PaymentModel Payment { get; set; }
        public Notification notification { get; set; }
        public MailRequest mailRequest { get; set; }
        public PaymentFormModel(ApplicationDbContext context)
        {
            _context = context;
        }
        // Data-binding with cshtml
        [BindProperty]
        public InputModel Input { get; set; }

        public int ToBuyId
        {
            get { return _toBuyId; }
            set { _toBuyId = value; }
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        public IActionResult OnGet(int Id)
        {
            ToBuyId = Id;

            return Page();
        }

        public IActionResult OnPostRedirectToDestination(int Id)
        {
            return RedirectToPage("ConfirmPayment", new { Id });
        }
    }
}
