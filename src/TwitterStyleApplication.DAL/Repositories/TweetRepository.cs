using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterStyleApplication.DAL.Contracts.Repositories;
using TwitterStyleApplication.Domain.Entities;

namespace TwitterStyleApplication.DAL.Repositories
{
	public class TweetRepository : ITweetRepository
	{
		private readonly DataDbContext _dataDbContext;

		public TweetRepository(DataDbContext dataDbContext)
		{
			_dataDbContext = dataDbContext;
		}
		public Tweet CreateTweet(Tweet tweet)
		{
			_dataDbContext.Tweets.Add(tweet);
			return tweet;
		}
	}
}
