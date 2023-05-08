using ETMP.Models;
using ETMP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETMP.Pages.Notifications
{
    public class NotificationsController : Controller
    {
        INotiService _notiService = null;
        List<Noti> _oNotifications = new List<Noti>();
        public NotificationsController(INotiService notiService)
        {
            _notiService = notiService;
        }
        public IActionResult AllNotifications()
        {
            return View();
        }
        public JsonResult GetNotifications(bool bIsGetOnlyUndread = false)
        {
            string nFromUserId = "a3f494b9-025e-4d40-af17-4e63e264cbfc";
            _oNotifications = new List<Noti>();
            _oNotifications = _notiService.GetNotifications(nFromUserId, bIsGetOnlyUndread);
            return Json(_oNotifications);
        }
    }
}
