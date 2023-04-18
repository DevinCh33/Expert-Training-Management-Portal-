using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ETMP.Pages
{
    public class EditTrainingModel : PageModel
    {
        public TrainingModel Training { get; set; }
        public string? TrainingName { get; set; }
        public int TrainingPrice { get; set; }
        public string? TrainingItinerary { get; set; }
        public string? TrainingVenue { get; set; }
        public string? TrainingCategory { get; set; }
        public Boolean Availability { get; set; }
        private readonly ApplicationDbContext _context;
        public EditTrainingModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id)
        {
            var training = _context.Trainings.FirstOrDefault(t => t.Id == id);

            // Bind the retrieved training data to the model properties
            Training = training;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var trainingToUpdate = await _context.Trainings.FindAsync(id);

            if (await TryUpdateModelAsync<TrainingModel>(
                trainingToUpdate,
                "training",   // Prefix for form value.
                  t => t.TrainingName, t => t.TrainingPrice, t => t.TrainingVenue, t => t.TrainingItinerary, t => t.TrainingCategory, t => t.Availability))
            {
                await _context.SaveChangesAsync();
              
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

