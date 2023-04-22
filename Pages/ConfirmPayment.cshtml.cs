using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ETMP.Data;
using ETMP.Models;
using Newtonsoft.Json;

namespace ETMP.Pages
{
    public class ConfirmPaymentModel : PageModel
    {
        [BindProperty]
        public TrainingModel Training { get; set; }
        public string ? PurchasedTraining { get; set; }
        public string? TrainingName { get; set; }
        public int TrainingPrice { get; set; }
        public string? TrainingItinerary { get; set; }
        public string? TrainingDescription { get; set; }
        public string? TrainingVenue { get; set; }
        public string? TrainingCategory { get; set; }
        public Boolean Availability { get; set; }
        private readonly ETMP.Data.ApplicationDbContext _context;

        public ConfirmPaymentModel(ETMP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var training = _context.Trainings.FirstOrDefault(t => t.Id == id);

            // Bind the retrieved training data to the model properties
            Training = training;

            PurchasedTraining = JsonConvert.SerializeObject(Training);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var trainingToUpdate = await _context.Trainings.FindAsync(id);

            if (await TryUpdateModelAsync<TrainingModel>(
                trainingToUpdate,
                "training",   // Prefix for form value.
                  t => t.TrainingName, t => t.TrainingPrice, t => t.TrainingVenue, t => t.TrainingItinerary, t => t.TrainingCategory, t => t.Availability, t => t.TrainingDescription))
            {
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
