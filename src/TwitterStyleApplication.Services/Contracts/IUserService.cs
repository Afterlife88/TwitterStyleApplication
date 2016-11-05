using System.Threading.Tasks;
using TwitterStyleApplication.Services.RequestModels;
using TwitterStyleApplication.Services.ServiceModels;

namespace TwitterStyleApplication.Services.Contracts
{
    public interface IUserService
    {
		ServiceState State { get; }
		Task<ServiceState> CreateAsync(RegistrationRequest model);
	}
}
