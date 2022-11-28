using H4App3API.Models;
using H4App3API.Services;
using Microsoft.AspNetCore.Mvc;

namespace H4App3API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserService _service;

		public UserController(IUserService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			try
			{
				List<User> userList = await _service.GetAllUsers();
				if (userList.Count > 0)
				{
					return Ok(userList);
				}
				return NoContent();
			}
			catch (Exception ex)
			{

				return Problem(ex.Message);
			}
		}
	}
}
