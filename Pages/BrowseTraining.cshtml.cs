using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ETMP.Pages
{
    //[Authorize(Roles = "Admin,Member")]
    public class BrowseTraining : PageModel
    {
        
        public List<TrainingModel> Trainings { get; set; }
        private readonly ApplicationDbContext _context;

        public BrowseTraining(ApplicationDbContext context)
        {
            
            _context = context;
            
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Trainings = await _context.Trainings.ToListAsync();
            return Page();
        }
        /*
        public async Task<IActionResult> OnGetTrainingAsync(string sortColumn = "TrainingName", string sortOrder = "asc")
        {
            var trainingsQuery = _context.Trainings.AsQueryable();

            switch (sortColumn.ToLower())
            {
                case "trainingname":
                    trainingsQuery = sortOrder.ToLower() == "desc"
                        ? trainingsQuery.OrderByDescending(t => t.TrainingName)
                        : trainingsQuery.OrderBy(t => t.TrainingName);
                    break;
                case "trainingprice":
                    trainingsQuery = sortOrder.ToLower() == "desc"
                        ? trainingsQuery.OrderByDescending(t => t.TrainingPrice)
                        : trainingsQuery.OrderBy(t => t.TrainingPrice);
                    break;
                    // add more cases for other sortable columns as needed
            }

            Trainings = await trainingsQuery.ToListAsync();

            return Page();
        }*/


    }
}


