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

        public ManageTrainingModel(ApplicationDbContext context)
        {
            _context = context;
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

        public IActionResult Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest();
            }

            var path = Path.Combine("uploads", filePath);
            var memory = new MemoryStream();
            using (var stream = new FileStream(Path.Combine("wwwroot", path), FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }

        public async Task<IActionResult> OnPostAddButton()
        {
            var file = Request.Form.Files.GetFile("TrainingMaterial");

            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("uploads", file.FileName);
                using (var stream = new FileStream(Path.Combine("wwwroot", filePath), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                Training.TrainingMaterialFilePath = filePath;
            }

            return RedirectToPage("/AddTraining", new { Training.TrainingName, Training.TrainingPrice, Training.TrainingCategory, Training.TrainingVenue, Training.Availability, Training.TrainingDescription, Training.TrainingStartDateTime, Training.TrainingEndDateTime, Training.TrainingMaterialFilePath });
        }

        public IActionResult OnPostCancelButton()
        {
            return RedirectToPage("/ManageTraining");
        }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            EditTraining = _context.Trainings.ToList();

            _dateNow = DateTime.Now;
            
            return Page();
        }
    }
}