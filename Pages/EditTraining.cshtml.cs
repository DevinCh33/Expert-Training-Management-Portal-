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
        public string? TrainingDescription { get; set; }
        public string? TrainingVenue { get; set; }
        public string? TrainingCategory { get; set; }
        public Boolean Availability { get; set; }
        private DateTime _dateNow;
        private readonly ApplicationDbContext _context;
        public Notification notification { get; set; }
        
        public EditTrainingModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public DateTime DateNow
        {
            get { return _dateNow; }
            set { _dateNow = value; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var training = _context.Trainings.FirstOrDefault(t => t.Id == id);

            // Bind the retrieved training data to the model properties
            Training = training;

            return Page();
        }

        public IActionResult OnPostCancelButton()
        {

            return RedirectToPage("/ManageTraining");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var trainingToUpdate = await _context.Trainings.FindAsync(id);
            
            if (await TryUpdateModelAsync<TrainingModel>(
                trainingToUpdate,
                "training",   // Prefix for form value.
                  t => t.TrainingName, t => t.TrainingPrice, t => t.TrainingVenue, t => t.TrainingCategory, t => t.Availability, t => t.TrainingDescription, t => t.TrainingStartDateTime, t => t.TrainingEndDateTime))
            {
                //notification
                
                if(Training != null)
                {
                    notification = new Notification();
                    notification.NotificationHeader = "Training Edited ";
                    notification.NotificationBody = trainingToUpdate.TrainingName + " is edited!";
                    notification.NotificationDate = DateTime.Now;
                    _context.Notification.Add(notification);
                    _context.SaveChanges();
                }
                //notification ended
                await _context.SaveChangesAsync();
                return RedirectToPage("./ManageTraining");
            }
            return Page();
        }
    }
}

