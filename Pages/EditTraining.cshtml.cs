using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#nullable disable

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest();
            }

            var path = Path.Combine("uploads", filePath);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var trainingToUpdate = await _context.Trainings.FindAsync(id);

            var file = Request.Form.Files.GetFile("TrainingMaterial");
            _logger.LogInformation("Checking File Length");
            _logger.LogInformation(file.Length.ToString());

            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("uploads", file.FileName);
                using (var stream = new FileStream(Path.Combine("wwwroot", filePath), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                trainingToUpdate.TrainingMaterialFilePath = filePath;
                _context.Entry(trainingToUpdate).Property(x => x.TrainingMaterialFilePath).IsModified = true; // Mark the property as modified

            }

            if (await TryUpdateModelAsync<TrainingModel>(
                trainingToUpdate,
                "training",   // Prefix for form value.
                  t => t.TrainingName, t => t.TrainingPrice, t => t.TrainingVenue, t => t.TrainingCategory, t => t.Availability, t => t.TrainingDescription, t => t.TrainingStartDateTime, t => t.TrainingEndDateTime, t => t.TrainingMaterialFilePath))
            {
                //notification
                

                // _context.SaveChangesAsync();
                await _context.SaveChangesAsync();
                return RedirectToPage("./ManageTraining");
            }
            return Page();
        }
    }
}

