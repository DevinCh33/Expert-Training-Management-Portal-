namespace ETMP.Models
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile>? Attachments { get; set; }

        public MailRequest(string ToEmail, string Subject, string Body, List<IFormFile>? Attachments)
        {
            this.ToEmail = ToEmail;
            this.Subject = Subject;
            this.Body = Body;
            this.Attachments = Attachments;
        }
    }
}
