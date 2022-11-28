using static System.Net.Mime.MediaTypeNames;
using System.Net.Mail;

namespace H4App3API.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int CardId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentFile { get; set; }

    }
}
