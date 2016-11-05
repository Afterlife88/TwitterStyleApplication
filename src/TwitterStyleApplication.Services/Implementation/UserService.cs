using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterStyleApplication.DAL.Contracts.Repositories;
using TwitterStyleApplication.Domain.Entities;
using TwitterStyleApplication.Services.Contracts;
using TwitterStyleApplication.Services.RequestModels;
using TwitterStyleApplication.Services.ServiceModels;
using TwitterStyleApplication.Services.ServiceModels.Enums;

namespace TwitterStyleApplication.Services.Implementation
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="userRepository"></param>
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
			State = new ServiceState();
		}

		/// <summary>
		/// Model state of the executed actions
		/// </summary>
		public ServiceState State { get; }

		public async Task<ServiceState> CreateAsync(RegistrationRequest modelDto)
		{
			if (modelDto.Email.Split(' ').Length == 2)
			{
				State.ErrorMessage = "Email should not contain spaces!";
				State.TypeOfError = TypeOfServiceError.BadRequest;
				return State;
			}
			if (string.IsNullOrWhiteSpace(modelDto.Password))
			{
				State.ErrorMessage = "You must type a password.";
				State.TypeOfError = TypeOfServiceError.BadRequest;
				return State;
			}
			var checkIsUserAlreadyExistWithEmail = await _userRepository.GetUserAsync(modelDto.Email);
			if (checkIsUserAlreadyExistWithEmail != null)
			{
				State.ErrorMessage = "User with requested email already exist!";
				State.TypeOfError = TypeOfServiceError.BadRequest;
				return State;
			}
			// Create user
			var user = new ApplicationUser()
			{
				Email = modelDto.Email,
				UserName = modelDto.Email,
				Tweets = new List<Tweet>()
			};
			await _userRepository.CreateAsync(user, modelDto.Password);
			return State;
		}
	}
}

