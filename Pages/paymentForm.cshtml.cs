using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ETMP.Pages
{
    public class PaymentFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        //Before asp validation addition
        [BindProperty]
        public PaymentInput Payment { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int ToBuyId { get; set; }

        public PaymentFormModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int Id)
        {
            //Payment = new PaymentModel();
            ToBuyId = Id;
        }

        public IActionResult OnPost(int Id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Perform payment processing and save to the database
            // ...

            return RedirectToPage("ConfirmPayment", new {Id}); // Redirect to a confirmation page
        }
    }

    public class PaymentInput
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

        [Required(ErrorMessage = "Zip Code is required")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "Please enter a valid zip code")]
        public int? ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter the name on your card")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "Please enter your Card Number")]
        [RegularExpression(@"^(?:[0-9]{15,17})$", ErrorMessage = "Please enter a valid credit card number")]
        public string CardNum { get; set; }

        [Required(ErrorMessage = "Please enter the card's expiry month")]
        [RegularExpression(@"^(?:[1-9]|1[0-2])$", ErrorMessage = "Please enter a valid month")]
        public int? CardExpiryMonth { get; set; }

        [Required(ErrorMessage = "Please enter the card's expiry year")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter a valid Year")]
        public int? CardExpiryYear { get; set; }

        [Required(ErrorMessage = "Please enter the card's CVV")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Please enter the card's CVV")]
        public int? CardCVV { get; set; }
        /*0
        [Required(ErrorMessage = "Please enter your ZIP code")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Please enter a valid ZIP code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter your card number")]
        [CreditCard(ErrorMessage = "Please enter a valid credit card number")]
        public string CardNum { get; set; }

        [Required(ErrorMessage = "Please enter the card's expiration month")]
        [RegularExpression(@"^(0[1-9]|1[0-2])$", ErrorMessage = "Please enter a valid expiration month")]
        public string CardExpiryMonth { get; set; }

        [Required(ErrorMessage = "Please enter the card's expiration year")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter a valid expiration year")]
        public string CardExpiryYear { get; set; }

        [Required(ErrorMessage = "Please enter the card's CVV")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Please enter a valid CVV")]
        public string CardCVV { get; set; }
        */
    }
}
