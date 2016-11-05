using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterStyleApplication.Services.DTO;
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
		Task<RelationshipDto> GetRelationship(string callerEmail);

		Task<IEnumerable<UserDTO>> AllUser(string callerEmail);
	}
}
