using System.Threading.Tasks;
using TwitterStyleApplication.DAL.Contracts;
using TwitterStyleApplication.DAL.Contracts.Repositories;

namespace TwitterStyleApplication.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private DataDbContext DataDbContext { get; }
		public IUserRepository UserRepository { get; }
		public ITweetRepository TweetRepository { get; }
		#region Constructors / Destructors

		public UnitOfWork(DataDbContext dataDbContext,
			ITweetRepository tweetRepository, IUserRepository userRepository)
		{
			DataDbContext = dataDbContext;
			UserRepository = userRepository;
			TweetRepository = tweetRepository;
		}

		#endregion

		/// <summary>
		/// Save pending changes to the database
		/// </summary>
		public async Task<int> CommitAsync()
		{
			return await DataDbContext.SaveChangesAsync();
		}

	}
}
