using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ETMP.Data;
using ETMP.Models;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class ItineraryModel : PageModel
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
    }
}

