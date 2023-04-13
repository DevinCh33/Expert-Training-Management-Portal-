using ETMP.Data;
using ETMP.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class BrowseTrainingModel : PageModel
    {
        private readonly ILogger<BrowseTrainingModel> _logger;
        private readonly ApplicationDbContext _context;

        public List<TrainingModel> Trainings { get; set; }

        public BrowseTrainingModel(ILogger<BrowseTrainingModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Trainings = _context.Trainings.ToList();
        }
    }
}

