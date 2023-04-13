using Microsoft.AspNetCore.Mvc.RazorPages;
using ETMP.Data;
using ETMP.Model;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ETMP.Pages
{
    public class EditTrainingModel : PageModel
    {
        [BindProperty]
        public TrainingModel Training { get; set; }
        public IActionResult OnPostAddButton()
        {
            return RedirectToPage("/AddTraining", new { Training.TrainingName, Training.TrainingPrice, Training.TrainingItinerary, Training.TrainingCategory, Training.TrainingVenue, Training.Availability });
        }

        public void OnGet()
        {
            /*
            if (string.IsNullOrEmpty(training.TrainingName))
            {
                training.TrainingName = "TestTraining12334";
            }

            if (string.IsNullOrEmpty(training.TrainingItinerary))
            {
                training.TrainingItinerary = "TrainingItinerary234545";
            }

            if (string.IsNullOrEmpty(training.TrainingVenue))
            {
                training.TrainingVenue = "TrainingVenue2231";
            }

            if (string.IsNullOrEmpty(training.TrainingCategory))
            {
                training.TrainingCategory = "TrainingCategory123221";
            }

            TrainingModel trainingModel1 = new TrainingModel();
            trainingModel1.TrainingName = training.TrainingName;
            trainingModel1.TrainingPrice = training.TrainingPrice;
            trainingModel1.TrainingItinerary = training.TrainingItinerary;
            trainingModel1.TrainingVenue = training.TrainingVenue;
            trainingModel1.TrainingCategory = training.TrainingCategory;
            trainingModel1.Availability = training.Availability;

            _context.Trainings.Add(trainingModel1);
            _context.SaveChanges();
            */
        }
    }
}
