using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

		public async Task<IEnumerable<Tweet>> GetReleatedTweets(string userId)
		{
			var userData = await _dataDbContext.Users.Include(r => r.Following).FirstOrDefaultAsync(r => r.Id == userId);
			var publishers = userData.Following.ToArray();
			var publishersIds = userData.Following.Select(p => p.SubscriberId);
			var tweets =
				await _dataDbContext.Tweets.Include(r => r.Author).Where(s => publishersIds.Contains(s.Author.Id) || s.Author == userData).ToArrayAsync();

			return tweets;
		}
	}
}
