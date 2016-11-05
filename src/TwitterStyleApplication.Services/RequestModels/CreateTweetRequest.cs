using System.ComponentModel.DataAnnotations;

namespace TwitterStyleApplication.Services.RequestModels
{
    public class CreateTweetRequest
    {
		[MaxLength(250)]
		public string MesssageData { get; set; }

    }
}
