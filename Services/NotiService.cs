using Dapper;
using ETMP.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace ETMP.Services
{
    public class NotiService:INotiService
    {
        List<Noti> _oNotifications = new List<Noti>();
        public List<Noti> GetNotifications(string nFromUserId, bool bIsGetOnlyUnread)
        {
            _oNotifications = new List<Noti>();
            using (DbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                var oNotis = con.Query<Noti>("SELECT * FROM VIEW_Notification WHERE FromUserId="+ nFromUserId).ToList();
                if (oNotis != null && oNotis.Count() > 0 )
                {
                    _oNotifications = oNotis;
                }
                return _oNotifications;
            }
        }
    }
}
