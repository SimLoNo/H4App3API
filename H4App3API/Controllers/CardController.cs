using H4App3API.Database.DTO;
using H4App3API.Models;
using H4App3API.Services;
using Microsoft.AspNetCore.Mvc;

namespace H4App3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {


            private readonly ICardService _service;

            public CardController(ICardService service)
            {
                _service = service;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllCards()
            {
                try
                {
                    List<Card> cardList = await _service.GetAllCards();
                    if (cardList.Count > 0)
                    {
                        return Ok(cardList);
                    }
                    return NoContent();
                }
                catch (Exception ex)
                {

                    return Problem(ex.Message);
                }
            }

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateStatusOnCard([FromRoute] int id, [FromBody] Card card)
		{
			if (card.CardId > 0)
			{
				try
				{
					Card updatedCard = await _service.UpdateStatusOnCard(id, card);
					if (updatedCard is not null)
					{
						return Ok(updatedCard);
					}
					return NotFound();
				}
				catch (Exception ex)
				{

					return Problem(ex.Message);
				}

			}
			return Problem("invalid request");
		}


		[HttpPost]
		public async Task<IActionResult> CreateCard([FromBody] CardRequest newCard)
		{
			try
			{
				Card createdCard = await _service.AddCard(newCard);
				if (createdCard is null)
				{
					return NotFound();
				}
				return Ok(createdCard);
			}
			catch (Exception ex)
			{

				return Problem(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCard([FromRoute] int id)
		{
			if (id > 0)
			{
				try
				{
					Card deletedCard = await _service.DeleteCard(id);
					if (deletedCard is not null)
					{
						return Ok(deletedCard);
					}
					return NotFound();
				}
				catch (Exception ex)
				{

					return Problem(ex.Message);
				}
			}
			return BadRequest("Id not valid");
		}

		//[HttpGet("{id}")]
		//public async Task<IActionResult> GetAuthorById([FromRoute] int id)
		//{
		//    try
		//    {
		//        AuthorResponse author = await _service.GetAuthorById(id);
		//        if (author != null)
		//        {
		//            return Ok(author);
		//        }
		//        return NotFound();
		//    }
		//    catch (Exception ex)
		//    {

		//        return Problem(ex.Message);
		//    }
		//}


		//[HttpPut("{id}")]
		//public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorRequest author)
		//{
		//    if (id <= 0)
		//    {
		//        return BadRequest();
		//    }
		//    try
		//    {
		//        AuthorResponse authorResult = await _service.UpdateExistingAuthor(id, author);
		//        if (authorResult != null)
		//        {
		//            return Ok(authorResult);
		//        }
		//        return NotFound();
		//    }
		//    catch (Exception ex)
		//    {

		//        return Problem(ex.Message);
		//    }
		//}

		//[HttpDelete("{id}")]
		//public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
		//{
		//    try
		//    {
		//        AuthorResponse authorResult = await _service.DeleteAuthor(id);

		//        if (authorResult != null)
		//        {
		//            return Ok(authorResult);
		//        }
		//        return NotFound();
		//    }
		//    catch (Exception ex)
		//    {

		//        return Problem(ex.Message);
		//    }
		//}
	}
}
