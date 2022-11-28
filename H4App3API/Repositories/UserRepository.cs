using H4App3API.Database;
using H4App3API.Models;
using Microsoft.EntityFrameworkCore;

namespace H4App3API.Repositories
{
	public interface IUserRepository
	{
		Task<List<User>> GetAllUsers();
	}
	public class UserRepository : IUserRepository
	{
		private readonly ScrumContext _context;
		public UserRepository(ScrumContext context)
		{
			_context= context;
		}
		public async Task<List<User>> GetAllUsers()
		{
			return await _context.UserTable.ToListAsync();
		}
	}
}
