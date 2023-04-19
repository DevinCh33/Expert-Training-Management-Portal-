using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETMP.Data;
using ETMP.Models;

namespace ETMP.Pages
{
    public class EditModel : PageModel
    {
        private readonly ETMP.Data.ApplicationDbContext _context;

        public EditModel(ETMP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TrainingModel TrainingModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trainings == null)
            {
                return NotFound();
            }

            var trainingmodel =  await _context.Trainings.FirstOrDefaultAsync(m => m.Id == id);
            if (trainingmodel == null)
            {
                return NotFound();
            }
            TrainingModel = trainingmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TrainingModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingModelExists(TrainingModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TrainingModelExists(int id)
        {
          return (_context.Trainings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
