using System.Threading.Tasks;
using TwitterStyleApplication.DAL.Contracts.Repositories;

namespace TwitterStyleApplication.DAL.Contracts
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; }
		ITweetRepository TweetRepository { get; }
		DataDbContext DataDbContext { get; }
		/// <summary>
		/// Save changes in database
		/// </summary>
		/// <returns></returns>
		Task<int> CommitAsync();
	}
}
