using Dapper;
using ETMP.Data;
using ETMP.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace ETMP.Services
{
    public class NotiService : INotiService
    {
        List<Noti>? _oNotifications {get;set;}
        private readonly ApplicationDbContext _context;
        public NotiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Noti> GetNotifications(string nFromUserId, bool bIsGetOnlyUnread)
        {
            var oNotis = _context.Set<Noti>()
                .FromSqlRaw("SELECT * FROM [Identity].[Notifications] WHERE FromUserId = {0}", nFromUserId)
                .ToList();
            return oNotis;
        }

    }
}