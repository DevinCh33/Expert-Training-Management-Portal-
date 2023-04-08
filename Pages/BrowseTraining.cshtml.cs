using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ETMP.Model;
using ETMP.Data;

namespace ETMP.Pages
{
    public class BrowseTrainingModel : PageModel
    {
        public List<TrainingModel> Trainings = new List<TrainingModel>();

        private readonly ILogger<BrowseTrainingModel> _logger;
        private readonly ApplicationDbContext _context;

        public BrowseTrainingModel(ILogger<BrowseTrainingModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void OnGet()
        {
            //Trainings = _context.Trainings.ToList();
        }
    }
}
