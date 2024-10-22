﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin")]
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

                _context.Trainings.Remove(TrainingModel);               
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ManageTraining");
        }
    }
}
