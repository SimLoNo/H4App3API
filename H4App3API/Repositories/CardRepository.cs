using H4App3API.Database;
using H4App3API.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace H4App3API.Repositories
{
    public interface ICardRepository
    {
        //Task<Card> CreateCard(Card Card);
        Task<List<Card>> GetAllCards();
        Task<Card> UpdateStatusOnCard(int id, Card Card);
		Task<Card> AddCard(Card card);
		Task<Card> DeleteCard(int cardId);
		//Task<Card> GetCardById(int CardId);
		//Task<List<Card>> GetCardsByNationality(int nationalityId);
		//Task<Card> UpdateExistingCard(int CardId, Card Card);
		//Task<Card> DeleteCard(int CardId); //jeg har på et tidspunkt kaldt den DeleteCardById: måske vigtigt
	}
    public class CardRepository : ICardRepository
    {
        private readonly ScrumContext _context;

        public CardRepository(ScrumContext context)
        {
            _context = context;
        }

        //CREATE
        //public async Task<Card> CreateCard(Card Card)
        //{
        //    // fjerne de medsendte Titler som kun indeholder id, så der kan indsættet de rigtige Titler.
        //    foreach (Title title in Card.Titles.ToList())
        //    {
        //        Title newTitle = _context.Title.First(t => t.TitleId == title.TitleId);
        //        if (newTitle != null)
        //        {
        //            Card.Titles.Remove(title);
        //            Card.Titles.Add(newTitle);
        //        }
        //    }


        //    _context.Card.Add(Card); //Denne indeholder ikke en ID

        //    Console.WriteLine();

        //    await _context.SaveChangesAsync();
        //    return Card; //Denne indeholder en ID
        //}

        //READ
        public async Task<List<Card>> GetAllCards()
        {
            return await _context.CardTable
                .Include(c => c.AssignedUser)
                .ToListAsync();
        }

        public async Task<Card> UpdateStatusOnCard(int id, Card Card)
		{
			Card updateCard = await _context.CardTable
				.Include(u => u.AssignedUser)
				.FirstOrDefaultAsync(card => card.CardId == id);
			if (updateCard != null)
			{
				updateCard.CardStatus = Card.CardStatus;
				updateCard.CardDescription = Card.CardDescription;
				updateCard.UserId = Card.UserId;
				updateCard.Title= Card.Title;
				await _context.SaveChangesAsync();

				if (updateCard.CardStatus == "Done")
				{
					var mailMessage = new MimeMessage();
					mailMessage.From.Add(new MailboxAddress("Scrumboard", "scrumboardapp@snoerregaard.dk"));
					mailMessage.To.Add(new MailboxAddress("User", "dwaf@live.dk"));
					mailMessage.Subject = $"Card {updateCard.Title} has been finished";
					mailMessage.Body = new TextPart("")
					{
						Text = $"Card {updateCard.Title} Assigned to {updateCard.AssignedUser.UserName} has been moved to {updateCard.CardStatus}."
					};

					using (var smtpClient = new SmtpClient())
					{
						smtpClient.Connect("send.one.com", 465, true);
						smtpClient.Authenticate("scrumboardapp@snoerregaard.dk", "H4Kode123");
						smtpClient.Send(mailMessage);
						smtpClient.Disconnect(true);
					}
				}
			}
				return updateCard;
		}
		public async Task<Card> AddCard(Card card)
		{
			_context.Add(card);
			await _context.SaveChangesAsync();
			return card;
		}

		public async Task<Card> DeleteCard(int cardId)
		{
			Card deleteCard = await _context.CardTable.FirstOrDefaultAsync(c => c.CardId == cardId);
			if (deleteCard != null)
			{
				_context.Remove(deleteCard);
				await _context.SaveChangesAsync();
			}
			return deleteCard;
		}



		//public async Task<Card> GetCardById(int CardId)
		//{
		//    return await _context.Card
		//        .Include(a => a.Titles)
		//        .FirstOrDefaultAsync(Card => Card.CardId == CardId);
		//}

		//public async Task<List<Card>> GetCardsByNationality(int nationalityId)
		//{
		//    return await _context.Card
		//        .Include(a => a.Titles)
		//        .Where(Card => Card.NationalityId == nationalityId)
		//        .ToListAsync();
		//}

		//UPDATE
		//    public async Task<Card> UpdateExistingCard(int CardId, Card Card)
		//    {
		//        Card updateCard = await _context.Card
		//            .Include(t => t.Titles)
		//            .FirstOrDefaultAsync(Card => Card.CardId == CardId);
		//        if (updateCard != null)
		//        {
		//            updateCard.FName = Card.FName;
		//            updateCard.MName = Card.MName;
		//            updateCard.LName = Card.LName;
		//            updateCard.BYear = Card.BYear;
		//            updateCard.DYear = Card.DYear;
		//            updateCard.NationalityId = Card.NationalityId;

		//            // Køre igennem de titler der er sent med fra frontend, og kigger på om de hver især er tilføjet til forfatteren i databasen, og tilføjer dem hvis de ikke er
		//            foreach (Title sentTitle in Card.Titles)
		//            {
		//                if (updateCard.Titles.Exists(t => t.TitleId == sentTitle.TitleId) == false)
		//                {
		//                    Title newTitle = _context.Title.First(t => t.TitleId == sentTitle.TitleId);
		//                    if (newTitle != null)
		//                    {
		//                        updateCard.Titles.Add(newTitle);
		//                    }
		//                }
		//            }

		//            // Kigger alle de tilknyttede titler igennem der er i databasen, og hvis de ikke er i den tilsendte request, bliver forbindelsen fjernet i databasen.
		//            if (updateCard.Titles.Count > 0)
		//            {
		//                foreach (Title existingTitle in updateCard.Titles.ToList())
		//                {
		//                    if (Card.Titles.Exists(t => t.TitleId == existingTitle.TitleId) == false)
		//                    {
		//                        updateCard.Titles.Remove(existingTitle);

		//                    }
		//                }
		//            }
		//            await _context.SaveChangesAsync();
		//        }
		//        return updateCard;
		//    }

		//    //DELETE
		//    public async Task<Card> DeleteCard(int CardId)
		//    {
		//        Card deleteCard = await _context.Card
		//            .FirstOrDefaultAsync(Card => Card.CardId == CardId);
		//        if (deleteCard != null)
		//        {
		//            _context.Remove(deleteCard);
		//            await _context.SaveChangesAsync();
		//        }
		//        return deleteCard;
		//        //Virker ulogisk at man returnerer en Card, man lige har slettet.
		//        //det gør vi dog heller ikke rigtig.
		//    }

		//    public async Task<Card> DeleteCardById(int CardId)
		//    {
		//        Card deleteCard = await _context.Card.FirstOrDefaultAsync(Card => Card.CardId == CardId);
		//        if (deleteCard != null)
		//        {
		//            _context.Card.Remove(deleteCard);
		//            await _context.SaveChangesAsync();
		//        }
		//        return deleteCard;
		//    }
	}
}
