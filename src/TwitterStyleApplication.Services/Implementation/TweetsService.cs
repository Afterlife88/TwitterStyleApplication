﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TwitterStyleApplication.DAL.Contracts;
using TwitterStyleApplication.Domain.Entities;
using TwitterStyleApplication.Services.Contracts;
using TwitterStyleApplication.Services.DTO;
using TwitterStyleApplication.Services.RequestModels;
using TwitterStyleApplication.Services.ServiceModels;
using TwitterStyleApplication.Services.ServiceModels.Enums;

namespace TwitterStyleApplication.Services.Implementation
{
	public class TweetsService : ITweetsService
	{
		private readonly IUnitOfWork _unitOfWork;
		public ServiceState State { get; }

		public TweetsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			State = new ServiceState();
		}

		public async Task<IEnumerable<TweetDTO>> GetReleatedTweets(string userEmail)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetUserAsync(userEmail);

				var tweets = await _unitOfWork.TweetRepository.GetReleatedTweets(user.Id);

				
				return Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDTO>>(tweets);

			}
			catch (Exception ex)
			{
				State.ErrorMessage = ex.Message;
				State.TypeOfError = TypeOfServiceError.ServiceError;
				return null;
			}
		}
		public async Task<TweetDTO> CreateTweet(string userEmail, CreateTweetRequest request)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetUserAsync(userEmail);

				var tweet = new Tweet() { Author = user, DateCreated = DateTime.Now, MessageData = request.MesssageData };


				//user.Tweets.Add(tweet);
				 _unitOfWork.TweetRepository.CreateTweet(tweet);
				await _unitOfWork.CommitAsync();

				return Mapper.Map<Tweet, TweetDTO>(tweet);

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
