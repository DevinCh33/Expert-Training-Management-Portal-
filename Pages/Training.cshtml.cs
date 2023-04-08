using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class TrainingModel : PageModel
    {
        private readonly ILogger<TrainingModel> _logger;

        public TrainingModel(ILogger<TrainingModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
