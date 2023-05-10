namespace ETMP.Models
{
    public class Notification
    {
        public int? NotificationId { get; set; }
        public string? ToUserId { get; set; }
        public string? ToUserName { get; set; }
        public string? NotificationHeader { get; set; }
        public string? NotificationBody { get; set; }
        public DateTime NotificationDate { get; set; } // add this line
        public Boolean? IsRead { get; set; }
    }
}
