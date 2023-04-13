using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class UserEditTrainingModel : PageModel
    {
        private readonly ILogger<UserEditTrainingModel> _logger;

        public UserEditTrainingModel(ILogger<UserEditTrainingModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
