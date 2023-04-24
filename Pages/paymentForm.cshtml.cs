using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class PaymentFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string cType { get; set; }
        public string username { get; set; }
        public int cardNo { get; set; }
        public int expiration { get; set; }
        public int CVV { get; set; }

        public PaymentFormModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            PaymentModel payment = new PaymentModel();
            payment.cardNo = cardNo;
            payment.expiration = expiration;
            payment.CVV = CVV;

            _context.Payment.Add(payment);
            _context.SaveChanges();
        }
    }
}
