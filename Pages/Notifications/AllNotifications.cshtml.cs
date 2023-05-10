using ETMP.Data;
using ETMP.Models;
using ETMP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages.Notifications
{
    public class AllNotificationsModel : PageModel
    {
        private readonly INotiService _notiService;

        public AllNotificationsModel(INotiService notiService)
        {
            _notiService = notiService;
        }

        public IActionResult OnGet()
        {
            var notifications = _notiService.GetNotifications("495a2b60-2bf9-4bc7-a9da-90af81169b49", false);
            ViewData["Notifications"] = notifications;
            return Page();
        }
    }
}
