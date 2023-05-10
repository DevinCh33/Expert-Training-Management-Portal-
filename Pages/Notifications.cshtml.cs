using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ETMP.Models;
using ETMP.Data;
using Microsoft.EntityFrameworkCore;

namespace ETMP.Pages.Shared
{
    public class NotificationsModel : PageModel
    {
        
        public List<Notification> Notifications { get; set; }
        public readonly ApplicationDbContext _context;

        public NotificationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            Notifications = await _context.Notification.ToListAsync();

            return Page();
        }

    }
}

