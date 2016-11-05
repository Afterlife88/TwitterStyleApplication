using System.ComponentModel.DataAnnotations;

namespace TwitterStyleApplication.Services.RequestModels
{
    public class FollowUnfollowRequest
    {
		[Required]
		public string Username { get; set; }
    }
}
