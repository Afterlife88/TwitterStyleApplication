using System.Threading.Tasks;
using TwitterStyleApplication.Services.RequestModels;
using TwitterStyleApplication.Services.ServiceModels;

namespace TwitterStyleApplication.Services.Contracts
{
    public interface IUserService
    {
		ServiceState State { get; }
		Task<ServiceState> CreateAsync(RegistrationRequest model);
	    Task<ServiceState> FollowUser(string callerUserEmail, string followUserName);

	    Task<ServiceState> UnFollowUser(string callerEmail, string unfollowUserName);
    }
}
