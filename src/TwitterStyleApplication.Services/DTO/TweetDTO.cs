using System;

namespace TwitterStyleApplication.Services.DTO
{
	public class TweetDTO
	{
		public Guid Id { get; set; }
		public DateTime CreationDate { get; set; }
		public string Message { get; set; }
		public Guid? UserId { get; set; }
	}
}
