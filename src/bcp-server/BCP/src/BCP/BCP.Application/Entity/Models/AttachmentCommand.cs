namespace BCP.Application.Entity.Models
{
    public class AttachmentCommand
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string MediaType { get; set; }
    }
}
