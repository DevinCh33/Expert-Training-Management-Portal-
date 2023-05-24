using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ETMP.Models;
using Newtonsoft.Json;
using ETMP.Services;
using Microsoft.AspNetCore.Authorization;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
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
        public Notification notification { get; set; }
        public MailRequest mailRequest { get; set; }
        public MailService mailService { get; set; }
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
