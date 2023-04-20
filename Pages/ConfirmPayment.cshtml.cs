using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ETMP.Data;
using ETMP.Models;

namespace ETMP.Pages
{
    public class ConfirmPaymentModel : PageModel
    {
        private readonly ETMP.Data.ApplicationDbContext _context;

        public ConfirmPaymentModel(ETMP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TrainingModel TrainingModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Trainings == null || TrainingModel == null)
            {
                return Page();
            }

            _context.Trainings.Add(TrainingModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
