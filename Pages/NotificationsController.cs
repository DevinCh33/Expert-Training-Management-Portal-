using ETMP.Models;
using ETMP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETMP.Pages
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
            return ""
        }
        public JsonResult Getotifications(bool bIsGetOnlyUndread = false) {
            int nToUserId = 3;
            _oNotifications = new List<Noti>();
            _oNotifications = _notiService.GetNotifications(nToUserId, bIsGetOnlyUndread);
            return Json(_oNotifications);
        }
    }
}
