using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ETMP.Data;
using ETMP.Models;

namespace ETMP.Pages
{
    [BindProperties(SupportsGet = true)]
    public class AddTrainingModel : PageModel
    {
        public string? TrainingName { get; set; }
        public int TrainingPrice { get; set; }
        public string? TrainingItinerary { get; set; }
        public string? TrainingVenue { get; set; }
        public string? TrainingCategory { get; set; }
        public string? TrainingDescription { get; set; }
        public Boolean Availability { get; set; }
        public string IsAvailable { get; set; } = "";

        private readonly ApplicationDbContext _context;
        public AddTrainingModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(TrainingName))
            {
                TrainingName = "TestTraining12334";
            }

            if (string.IsNullOrEmpty(TrainingItinerary))
            {
                TrainingItinerary = "TrainingItinerary234545";
            }

            if (string.IsNullOrEmpty(TrainingVenue))
            {
                TrainingVenue = "TrainingVenue2231";
            }

            if (string.IsNullOrEmpty(TrainingCategory))
            {
                TrainingCategory = "TrainingCategory123221";
            }

            TrainingModel trainingModel = new TrainingModel();
            trainingModel.TrainingName = TrainingName;
            trainingModel.TrainingPrice = TrainingPrice;
            trainingModel.TrainingItinerary = TrainingItinerary;
            trainingModel.TrainingVenue = TrainingVenue;
            trainingModel.TrainingCategory = TrainingCategory;
            trainingModel.TrainingDescription = TrainingDescription;
            trainingModel.Availability = Availability;

            if(Availability == true)
            {
                IsAvailable = "Yes";
            } else
            {
                IsAvailable = "No";
            }
            _context.Trainings.Add(trainingModel);
            _context.SaveChanges();
            return Page();
            

        }

    }
}
