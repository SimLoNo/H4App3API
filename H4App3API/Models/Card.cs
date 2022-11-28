using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace H4App3API.Models
{
    public class Card
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string CardDescription { get; set; }
        public string CardStatus { get; set; }
        public Attachment[] ?Attachment { get; set; }
        public User AssignedUser { get; set; }


    }
}
