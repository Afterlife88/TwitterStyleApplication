using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TwitterStyleApplication.DAL.Contracts.Repositories;
using TwitterStyleApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TwitterStyleApplication.DAL.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly DataDbContext _dbContext;
		public UserRepository(UserManager<ApplicationUser> userManager, DataDbContext dbContext)
		{
			_userManager = userManager;
			_dbContext = dbContext;
		}

		public async Task<ApplicationUser> GetUserById(string userId)
		{
			return await _userManager.FindByIdAsync(userId);
		}
		public async Task<ApplicationUser> GetUserAsync(string userEmail)
		{
			return await _userManager.FindByEmailAsync(userEmail);
		}
		public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
		{
			return await _userManager.Users.ToArrayAsync();
		}

		public async Task CreateAsync(ApplicationUser user, string password)
		{
			var a = await _userManager.CreateAsync(user, password);
		}

		public async Task EditAsync(ApplicationUser user)
		{
			await _userManager.UpdateAsync(user);
		}
		public async Task<ApplicationUser> GetUserByNameAsync(string username)
		{
			return await _dbContext.Users.Where(r => r.UserName == username)
				.Include(r => r.Followers)
				.Include(s => s.Following)
				.FirstOrDefaultAsync();
			//return await _userManager.FindByNameAsync(username);
		}
	}
}
