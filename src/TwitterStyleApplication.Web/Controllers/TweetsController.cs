using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterStyleApplication.Services;
using TwitterStyleApplication.Services.Contracts;
using TwitterStyleApplication.Services.DTO;
using TwitterStyleApplication.Services.RequestModels;

namespace TwitterStyleApplication.Web.Controllers
{
	[Route("api/tweets")]
	[Authorize(ActiveAuthenticationSchemes = "Bearer")]
	public class TweetsController : Controller
	{
		private readonly ITweetsService _tweetsService;
		public TweetsController(ITweetsService tweetsService)
		{
			_tweetsService = tweetsService;
		}
		// GET: api/tweets
		/// <summary>
		/// Recive all releated tweets to user
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<TweetDTO>), 200)]
		[ProducesResponseType(typeof(BadRequestResult), 400)]
		[ProducesResponseType(typeof(UnauthorizedResult), 401)]
		[ProducesResponseType(typeof(InternalServerErrorResult), 500)]
		public async Task<IActionResult> Get()
		{
			try
			{
				var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;

				var serviceResponse = await _tweetsService.GetReleatedTweets(userEmail);

				if (!_tweetsService.State.IsValid)
					return ServiceResponseDispatcher.ExecuteServiceResponse(this, _tweetsService.State.TypeOfError,
						_tweetsService.State.ErrorMessage);

				return Ok(serviceResponse);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);

			}
		}


		// POST api/tweets
		/// <summary>
		/// Create a tweet
		/// </summary>
		/// <param name="request"></param>
		[HttpPost]
		[ProducesResponseType(typeof(OkResult), 200)]
		[ProducesResponseType(typeof(BadRequestResult), 400)]
		[ProducesResponseType(typeof(UnauthorizedResult), 401)]
		[ProducesResponseType(typeof(InternalServerErrorResult), 500)]
		public async Task<IActionResult> PostTweet([FromBody]CreateTweetRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;

				var serviceResponse = await _tweetsService.CreateTweet(userEmail, request);

				if (!_tweetsService.State.IsValid)
					return ServiceResponseDispatcher.ExecuteServiceResponse(this, _tweetsService.State.TypeOfError,
						_tweetsService.State.ErrorMessage);

				return Ok(serviceResponse);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);

			}
		}


	}
}
