using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETMP.Pages
{
    public class PaymentFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public PaymentModel Payment { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ToBuyId { get; set; } // Add the ToBuyId property

        public PaymentFormModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int Id)
        {
            // Initialize the Payment model and other properties as needed
            Payment = new PaymentModel();
            ToBuyId = Id; // Assign the value to the ToBuyId property
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Perform payment processing and save to the database
            // ...

            return RedirectToPage("ConfirmationPage"); // Redirect to a confirmation page
        }
    }

    public class PaymentModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your state")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your ZIP code")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Please enter a valid ZIP code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter the name on your card")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "Please enter your card number")]
        [CreditCard(ErrorMessage = "Please enter a valid credit card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter the card's expiration month")]
        [RegularExpression(@"^(0[1-9]|1[0-2])$", ErrorMessage = "Please enter a valid expiration month")]
        public string ExpiryMonth { get; set; }

        [Required(ErrorMessage = "Please enter the card's expiration year")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter a valid expiration year")]
        public string ExpiryYear { get; set; }

        [Required(ErrorMessage = "Please enter the card's CVV")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Please enter a valid CVV")]
        public string CVV { get; set; }

        // Add additional payment properties as needed
    }
}
