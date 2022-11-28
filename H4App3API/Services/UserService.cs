using H4App3API.Models;
using H4App3API.Repositories;

namespace H4App3API.Services
{
	public interface IUserService
	{
		Task<List<User>> GetAllUsers();
	}
	public class UserService : IUserService
	{ 
		private readonly IUserRepository _repository;
		public UserService(IUserRepository repository)
		{
			_repository= repository;
		}
		public async Task<List<User>> GetAllUsers()
		{
			return await _repository.GetAllUsers();
		}
	}
}
