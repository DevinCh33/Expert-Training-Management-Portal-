using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ETMP.Models;
using ETMP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace ETMP.Pages.Shared
{
    [Authorize(Roles = "Admin, Member")]
    public class NotificationsModel : PageModel
    {
        public List<Notification> Notifications { get; set; }
        public readonly ApplicationDbContext _context;
        //newly added
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        //public Notification notification { get; set; }

        public NotificationsModel(ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Notifications = await _context.Notification
    .Where(n => n.ToUserId == user.Id)
    .ToListAsync();
            
            /*
            var training = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);
            DateTime currentDate = DateTime.Now;
            DateTime trainingStartDateTime = training[0].TrainingStartDateTime;
            DateTime nextSevenDays = currentDate.AddDays(7);

            if (trainingStartDateTime <= nextSevenDays)
            {
                notification = new Notification();
                notification.NotificationHeader = "Training starting within 7 days";
                notification.NotificationBody = "Training starting within 7 days - Body";
                notification.NotificationDate = DateTime.Now;
                _context.Notification.Add(notification);
            }*/



            return Page();
        }

    }
}

