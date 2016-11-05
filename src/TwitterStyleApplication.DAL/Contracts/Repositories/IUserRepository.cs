using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterStyleApplication.Domain.Entities;

namespace TwitterStyleApplication.DAL.Contracts.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
		Task CreateAsync(ApplicationUser user, string password);
		Task<ApplicationUser> GetUserByNameAsync(string username);
		Task EditAsync(ApplicationUser user);
		Task<ApplicationUser> GetUserAsync(string userEmail);
		Task<ApplicationUser> GetUserById(string userId);
	}
}
