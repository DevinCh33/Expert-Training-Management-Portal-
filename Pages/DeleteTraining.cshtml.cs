using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ETMP.Data;
using ETMP.Models;

namespace ETMP.Pages
{
    public class DeleteTrainingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Boolean isAvailable { get; set; }
        
        public DeleteTrainingModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Notification notification { get; set; }

        [BindProperty]
      public TrainingModel TrainingModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trainings == null)
            {
                return NotFound();
            }

            var trainingmodel = await _context.Trainings.FirstOrDefaultAsync(m => m.Id == id);

            isAvailable = trainingmodel.Availability;

            if (trainingmodel == null)
            {
                return NotFound();
            }
            else
            {
                TrainingModel = trainingmodel;

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Trainings == null)
            {
                return NotFound();
            }
            var trainingmodel = await _context.Trainings.FindAsync(id);

            if (trainingmodel != null)
            {
                TrainingModel = trainingmodel;
                notification = new Notification();
                notification.NotificationHeader = "Training removed!";
                notification.NotificationBody = "Training " + trainingmodel.TrainingName + " has been removed!";
                notification.NotificationDate = DateTime.Now;
                _context.Notification.Add(notification);
                _context.Trainings.Remove(TrainingModel);               
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ManageTraining");
        }
    }
}
