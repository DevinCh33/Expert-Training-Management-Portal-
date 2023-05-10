namespace ETMP.Models
{
    public class Noti
    {
        public int NotiId { get; set; } = 0;
        public string FromUserId { get; set; } = "";
        public string FromUserName { get; set; } = "";
        public string ToUserId { get; set; } = "";
        public string ToUserName { get; set; } = "";
        public string NotiHeader { get; set; } = "";
        public string NotiBody { get; set; } = "";
        public string Url { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedDateSt => this.CreatedDate.ToString("dd-MMM-yyyy HH:mm:ss");

    }
}
