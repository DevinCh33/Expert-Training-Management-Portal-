using Dapper;
using ETMP.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ETMP.Services
{
    public class NotiService:INotiService
    {
        List<Noti> _oNotifications = new List<Noti>();
        public List<Noti> GetNotifications(int nToUserId, bool bIsGetOnlyUnread)
        {
            _oNotifications = new List<Noti>();
            using (IDbConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=aspnet-ETMP-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                var oNotis = con.Query<Noti>("SELECT * FROM VIEW_Notification WHERE ToUserId="+ nToUserId).ToList();
                if (oNotis != null && oNotis.Count() > 0 )
                {
                    _oNotifications = oNotis;
                }
                return _oNotifications;
            }
        }
    }
}
