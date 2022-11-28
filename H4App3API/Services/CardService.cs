using H4App3API.Database.DTO;
using H4App3API.Models;
using H4App3API.Repositories;

namespace H4App3API.Services
{
    public interface ICardService
    {
        Task<List<Card>> GetAllCards();
		Task<Card> UpdateStatusOnCard(int id, Card card);
		Task<Card> AddCard(CardRequest card);
		Task<Card> DeleteCard(int id);
	}
    public class CardService : ICardService
    {
        private readonly ICardRepository _repository;

        public CardService(ICardRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Card>> GetAllCards()
        {
            return await _repository.GetAllCards();
        }

		public async Task<Card> UpdateStatusOnCard(int id, Card card)
        {
            return await _repository.UpdateStatusOnCard(id, card);
		}
		public async Task<Card> AddCard(CardRequest cardRequest)
        {
            Card card = MapCardRequestToCard(cardRequest);
            return await _repository.AddCard(card);
        }

        private Card MapCardRequestToCard(CardRequest card)
        {
            return new()
            {

                UserId = card.UserId,
                Title = card.Title,
                CardDescription = card.CardDescription,
                CardStatus = card.CardStatus,
                Attachment = card.Attachment,
                AssignedUser = card.AssignedUser
            };
        }
        public async Task<Card> DeleteCard(int id)
        {
            return await _repository.DeleteCard(id);
        }
	}
}
