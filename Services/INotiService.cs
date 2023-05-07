using ETMP.Models;

namespace ETMP.Services
{
    public interface INotiService
    {
        List<Noti> GetNotifications(int nToUserId, bool bIsGetOnlyUnread);
    }
}
