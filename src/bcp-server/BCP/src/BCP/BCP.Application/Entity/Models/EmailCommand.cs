namespace BCP.Application.Entity.Models
{
    public class EmailCommand
    {
        public string Object { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; } = new();
        public List<string> CC { get; set; } = new();
        public List<AttachmentCommand> Attachments { get; set; } = new();
        public bool? NoResponse { get; set; } = false;
    }
}
