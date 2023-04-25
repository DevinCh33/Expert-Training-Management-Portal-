using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class PaymentFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PaymentFormModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaymentModel Payment { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _context.Payment.AddAsync(Payment);
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            else
            {
                return Page();
            }
        }
    }
}
