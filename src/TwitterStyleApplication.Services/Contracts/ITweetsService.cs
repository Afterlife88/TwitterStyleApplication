using System.Threading.Tasks;
using TwitterStyleApplication.Services.DTO;
using TwitterStyleApplication.Services.RequestModels;
using TwitterStyleApplication.Services.ServiceModels;

namespace TwitterStyleApplication.Services.Contracts
{
	public interface ITweetsService
	{
		ServiceState State { get; }
		Task<TweetDTO> CreateTweet(string userEmail, CreateTweetRequest request);
	}
}
