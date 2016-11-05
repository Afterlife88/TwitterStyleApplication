using System.Threading.Tasks;
using TwitterStyleApplication.Domain.Entities;

namespace TwitterStyleApplication.DAL.Contracts.Repositories
{
	public interface ITweetRepository
	{
		Tweet CreateTweet(Tweet tweet);
	}
}
