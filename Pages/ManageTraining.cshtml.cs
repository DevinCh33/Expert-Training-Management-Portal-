using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ETMP.Data;
using ETMP.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

#nullable disable

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin")]
    public class ManageTrainingModel : PageModel
    {
        [BindProperty]
        public TrainingModel Training { get; set; }
        private DateTime _dateNow;
        public List<TrainingModel> EditTraining { get; set; } = new List<TrainingModel>();
        public List<SelectListItem> TrainingNames { get; set; }

        public string TrainingName { get; set; }
        public string TrainingMaterialFilePath { get; set; }

        [BindProperty]
        public string SelectedTraining { get; set; }

        private readonly ApplicationDbContext _context;

        private readonly ILogger<IndexModel> _logger;

        public ManageTrainingModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public DateTime DateNow
        {
            get { return _dateNow; }
            set { _dateNow = value; }
        }

        public IActionResult OnPostRedirectToNewPage()
        {
            // Set the Id in TempData
            TempData["Id"] = _context.Trainings;

            // Redirect to the new page
            return RedirectToPage("/EditTraining");
        }

        public async Task<IActionResult> OnPostAddButton()
        {
            var file = Request.Form.Files.GetFile("TrainingMaterial");
            _logger.LogInformation("Checking File Length");
            _logger.LogInformation(file.Length.ToString());

            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                Training.TrainingMaterialFilePath = filePath;
            }
            //await _context.SaveChangesAsync();
            Training.TrainingItinerary = "To be Removed";
            return RedirectToPage("/AddTraining", new { Training.TrainingName, Training.TrainingPrice, Training.TrainingItinerary, Training.TrainingCategory, Training.TrainingVenue, Training.Availability, Training.TrainingDescription, Training.TrainingStartDateTime, Training.TrainingEndDateTime, Training.TrainingMaterialFilePath });
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

            _dateNow = DateTime.Now;
            
            return Page();
        }
    }
}