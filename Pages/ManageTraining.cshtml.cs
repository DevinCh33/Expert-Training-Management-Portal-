using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ETMP.Data;
using ETMP.Models;
using System.Drawing;

#nullable disable

namespace ETMP.Pages
{
    public class ManageTrainingModel : PageModel
    {
        [BindProperty]
        public TrainingModel Training { get; set; }
        public List<TrainingModel> EditTraining { get; set; } = new List<TrainingModel>();
        public List<SelectListItem> TrainingNames { get; set; }

        public string TrainingName { get; set; }



        [BindProperty]
        public string SelectedTraining { get; set; }

        private readonly ApplicationDbContext _context;

        public ManageTrainingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPostAddButton()
        {
            return RedirectToPage("/AddTraining", new { Training.TrainingName, Training.TrainingPrice, Training.TrainingItinerary, Training.TrainingCategory, Training.TrainingVenue, Training.Availability });
        }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            TrainingNames = await _context.Trainings
                .Select(t => new SelectListItem
                {
                    Value = t.TrainingName,
                    Text = t.TrainingName

                })
                .ToListAsync();

            EditTraining = _context.Trainings.ToList();
            
            return Page();
        }
    }
}
        /*try
            {
                string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-ETMP-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM training";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrainingModel AddTrainingInfo = new TrainingModel();
                                AddTrainingInfo.Id = reader.GetInt32(0);
                                AddTrainingInfo.TrainingName = reader.GetString(1);
                                AddTrainingInfo.TrainingPrice = reader.GetInt32(2);
                                AddTrainingInfo.TrainingItinerary = reader.GetString(3);
                                AddTrainingInfo.TrainingVenue = reader.GetString(4);
                                AddTrainingInfo.TrainingCategory = reader.GetString(5);
                                AddTrainingInfo.Availability = reader.GetBoolean(6);

                                TrainingInfo.Add(AddTrainingInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                foreach(var name in Training.TrainingName)
                {

                }

                var training = await _context.Trainings
                    .FirstOrDefaultAsync(t => t.TrainingName == SelectedTraining);

                if (training != null)
                {
                    TrainingPrice = training.TrainingPrice;
                    TrainingItinerary = training.TrainingItinerary;
                    TrainingVenue = training.TrainingVenue;
                    TrainingCategory = training.TrainingCategory;
                    Availability = training.Availability;
                }
            }

            return Page();
        }
        */
