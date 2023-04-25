using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ETMP.Data;
using ETMP.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

#nullable disable

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult OnPostRedirectToNewPage()
        {
            // Set the Id in TempData
            TempData["Id"] = _context.Trainings;

            // Redirect to the new page
            return RedirectToPage("/EditTraining");
        }

        public IActionResult OnPostAddButton()
        {
            return RedirectToPage("/AddTraining", new { Training.TrainingName, Training.TrainingPrice, Training.TrainingItinerary, Training.TrainingCategory, Training.TrainingVenue, Training.Availability, Training.TrainingDescription });
        }

        public IActionResult OnPostCancelButton()
        {
            return RedirectToPage("/ManageTraining");
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