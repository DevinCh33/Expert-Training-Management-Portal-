using ETMP.Models;
using Microsoft.EntityFrameworkCore;

namespace ETMP.Services
{
    public interface INotiService
    {
        List<Noti> GetNotifications(string nFromUserId, bool bIsGetOnlyUnread);
    }


}
