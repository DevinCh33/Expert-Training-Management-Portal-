using ETMP.Models;

namespace ETMP.Services
{
    public interface INotiService
    {
        List<Noti> GetNotifications(string nFromUserId, bool bIsGetOnlyUndread);
    }
}
