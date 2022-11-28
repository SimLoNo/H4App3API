using H4App3API.Models;

namespace H4App3API.Database.DTO
{
	public class CardRequest
	{

		public int UserId { get; set; }
		public string Title { get; set; }
		public string CardDescription { get; set; }
		public string CardStatus { get; set; }
		public Attachment[] ?Attachment { get; set; }
		public User ?AssignedUser { get; set; }
	}
}
