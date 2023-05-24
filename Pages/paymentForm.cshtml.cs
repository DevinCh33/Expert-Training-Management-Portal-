using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class PaymentFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly Services.IMailService _mailService;
        private TrainingModel _training;
        private string _paymentMethod;
        private readonly UserManager<ETMPUser> _userManager;

        public PaymentFormModel(Services.IMailService mailService, ApplicationDbContext context, UserManager<ETMPUser> userManager)
        {
            _mailService = mailService;
            _context = context;
            _paymentMethod = "Credit Card";
            _userManager = userManager;
        }

        //Before asp validation addition
        [BindProperty]
        public PaymentInput Payment { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int ToBuyId { get; set; }

        public TrainingModel Training
        {
            get { return _training; }
            set { _training = value; }
        }

        public string PaymentMethod
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value; }
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
        }

        public IActionResult OnGet(int id)
        {
            _training = _context.Trainings.FirstOrDefault(t => t.Id == id);

            ToBuyId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostPayWithCash(int Id)
        {
            _training = _context.Trainings.FirstOrDefault(t => t.Id == Id);

            int length = Payment.CardNum.Length;
            string maskedCardNum = new string('*', length - 4) + Payment.CardNum.Substring(length - 4);
            _paymentMethod = "Pay with Cash";

            string subject = "Payment Receipt for - " + _training.TrainingName;
            string body = "<strong>Training Name: </strong>" + _training.TrainingName + "<br/><br/><strong>Price Paid: </strong> RM" + _training.TrainingPrice + "<br/><br/><strong>Name of Buyer: </strong>" + Payment.CardName + "<br/><br/><strong>Card Number: </strong>" + maskedCardNum + "<br/><br/><strong>Payment Method: </strong>" + _paymentMethod;

            string toEmail = "devinchp@gmail.com";

            var request = new MailRequest(toEmail, subject, body, null);

            // Sends Email
            await _mailService.SendEmailAsync(request);

            return RedirectToPage("/ConfirmPayment", new { Id });
        }

        public async Task<IActionResult> OnPostAsync(int Id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Saves card info

            PaymentModel paymentInfo = new PaymentModel();
            long card_number;
            long.TryParse(Payment.CardNum, out card_number);
            paymentInfo.cType = "Credit Card";
            paymentInfo.username = Payment.CardName;
            paymentInfo.cardNo = card_number;
            paymentInfo.expiration = Payment.CardExpiryMonth.ToString() + "/" + Payment.CardExpiryYear.ToString();
            paymentInfo.CVV = Payment.CardCVV;

            await _context.Payment.AddAsync(paymentInfo);
            await _context.SaveChangesAsync();

            var selectedTraining = _context.Trainings.FirstOrDefault(t => t.Id == Id);
            _training = selectedTraining;

            var user = await _userManager.GetUserAsync(User);
            int length = Payment.CardNum.Length;
            string maskedCardNum = new string('*', length - 4) + Payment.CardNum.Substring(length - 4);

            string subject = "Payment Receipt for - " + _training.TrainingName;
            string body = "<strong>Training Name: </strong>" + _training.TrainingName + "<br/><br/><strong>Price Paid: </strong> RM" + _training.TrainingPrice + "<br/><br/><strong>Name of Buyer: </strong>" + Payment.CardName + "<br/><br/><strong>Card Number: </strong>" + maskedCardNum + "<br/><br/><strong>Payment Method: </strong>" + _paymentMethod;

            string toEmail = user.Email;

            var request = new MailRequest(toEmail, subject, body, null);

            //Sends Email
            await _mailService.SendEmailAsync(request);

            return RedirectToPage("ConfirmPayment", new {Id}); // Redirect to a confirmation page
        }
    }



}
