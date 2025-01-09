namespace LavidaCoffee.Models
{
    public class MailRequest
    {
        public string ToEmail = "joe.lambon25@gmail.com";
        public required string CustomerEmail {  get; set; }
        public required string Subject { get ; set; }
        public required string Body { get; set; }
    }
}
