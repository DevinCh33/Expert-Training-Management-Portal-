using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETMP.Pages
{
    public class PaymentFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public PaymentModel Payment { get; set; }
        public PaymentFormModel(ApplicationDbContext context)
        {
            _context = context;
        }
        // Data-binding with cshtml
        [BindProperty]
        public InputModel Input { get; set; }
        
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /* public void OnGet()
         {
             PaymentModel payment = new PaymentModel();
             payment.cardNo = cardNo;
             payment.expiration = expiration;
             payment.CVV = CVV;

             _context.Payment.Add(payment);
             _context.SaveChanges();
         }*/
    }
}
