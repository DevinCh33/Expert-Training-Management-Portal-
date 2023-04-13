using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class EditTrainingModel : PageModel
    {
        private readonly ILogger<EditTrainingModel> _logger;

        public EditTrainingModel(ILogger<EditTrainingModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
