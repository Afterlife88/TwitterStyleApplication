using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TwitterStyleApplication.DAL.Contracts;
using TwitterStyleApplication.DAL.Contracts.Repositories;
using TwitterStyleApplication.Domain.Entities;
using TwitterStyleApplication.Services.Contracts;
using TwitterStyleApplication.Services.DTO;
using TwitterStyleApplication.Services.RequestModels;
using TwitterStyleApplication.Services.ServiceModels;
using TwitterStyleApplication.Services.ServiceModels.Enums;

namespace TwitterStyleApplication.Services.Implementation
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="userRepository"></param>
		/// <param name="unitOfWork"></param>
		public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
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
			var checkIfUserExistByUserName = await _userRepository.GetUserByNameAsync(modelDto.UserName);
			if (checkIfUserExistByUserName != null)
			{
				State.ErrorMessage = "User with user name already exist!";
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
				UserName = modelDto.UserName,
				//Tweets = new List<Tweet>()
			};
			await _userRepository.CreateAsync(user, modelDto.Password);
			return State;
		}

		public async Task<ServiceState> FollowUser(string callerUserEmail, string followUserName)
		{
			try
			{
				var subscriber = await _unitOfWork.UserRepository.GetUserAsync(callerUserEmail);

				var publisher = await _unitOfWork.UserRepository.GetUserByNameAsync(followUserName);

				if (publisher == null)
				{
					State.ErrorMessage = "Requested user to follow not found!";
					State.TypeOfError = TypeOfServiceError.NotFound;
					return State;
				}
				var getFollow = await _unitOfWork.DataDbContext.Subscriptions.FirstOrDefaultAsync(r => r.Subscriber.Id == publisher.Id &&
																								   r.Publisher.Id == subscriber.Id);

				if (getFollow != null)
				{
					State.ErrorMessage = "You already followed this user";
					State.TypeOfError = TypeOfServiceError.BadRequest;
					return State;
				}
				var subscription = new Subscription();
				subscription.Publisher = publisher;
				subscription.PublisherId = publisher.Id;
				subscription.Subscriber = subscriber;
				subscription.SubscriberId = subscriber.Id;

				subscriber.Following.Add(subscription);
				publisher.Followers.Add(subscription);

				await _unitOfWork.CommitAsync();
				return State;
			}
			catch (Exception ex)
			{
				State.ErrorMessage = ex.Message;
				State.TypeOfError = TypeOfServiceError.ServiceError;
				return State;
			}
		}

		public async Task<ServiceState> UnFollowUser(string callerEmail, string unfollowUserName)
		{
			try
			{
				var subscriber = await _unitOfWork.UserRepository.GetUserAsync(callerEmail);

				var publisher = await _unitOfWork.UserRepository.GetUserByNameAsync(unfollowUserName);

				if (publisher == null)
				{
					State.ErrorMessage = "Requested user to unfollow not found!";
					State.TypeOfError = TypeOfServiceError.NotFound;
					return State;
				}

				var getFollow = await _unitOfWork.DataDbContext.Subscriptions.FirstOrDefaultAsync(r => r.Subscriber.Id == publisher.Id &&
																									   r.Publisher.Id == subscriber.Id);

				subscriber.Followers.RemoveAll(p => p.PublisherId == publisher.Id);
				subscriber.Following.RemoveAll(s => s.SubscriberId == subscriber.Id);
				_unitOfWork.DataDbContext.Subscriptions.Remove(getFollow);
				//followUser.Following.Add(new Following() { FollowingUser = user, User = followUser });
				await _unitOfWork.CommitAsync();
				return State;

			}
			catch (Exception ex)
			{

				State.ErrorMessage = ex.Message;
				State.TypeOfError = TypeOfServiceError.ServiceError;
				return State;
			}
		}

		public async Task<RelationshipDto> GetRelationship(string callerEmail)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetUserAsync(callerEmail);

				var userWithReletionship = await _unitOfWork.UserRepository.GetUserByNameAsync(user.UserName);


				var followers = new List<ApplicationUser>();
				var following = new List<ApplicationUser>();
				foreach (var item in userWithReletionship.Followers.ToArray())
				{
					var usr = await _unitOfWork.UserRepository.GetUserById(item.PublisherId);
					followers.Add(usr);
				}
				foreach (var item in userWithReletionship.Following.ToArray())
				{
					var usr = await _unitOfWork.UserRepository.GetUserById(item.SubscriberId);
					following.Add(usr);
				}

				var response = new RelationshipDto()
				{

					Followers = (List<UserDTO>)Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDTO>>(followers),
					Following = (List<UserDTO>)Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDTO>>(following),

				};
				return response;

			}
			catch (Exception ex)
			{
				State.ErrorMessage = ex.Message;
				State.TypeOfError = TypeOfServiceError.ServiceError;
				return null;
			}

		}

		public async Task<IEnumerable<UserDTO>> AllUser(string callerEmail)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetUserAsync(callerEmail);

				var allUsers = await _unitOfWork.UserRepository.GetAllUsersAsync();

				var arr = allUsers.ToList();
				arr.Remove(user);
				return Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDTO>>(arr);
			}
			catch (Exception ex)
			{
				State.ErrorMessage = ex.Message;
				State.TypeOfError = TypeOfServiceError.ServiceError;
				return null;
			}
		}
	}
}



