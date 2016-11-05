using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterStyleApplication.Services;
using TwitterStyleApplication.Services.Contracts;
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
		[HttpGet]
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

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/tweets
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		[HttpPost]
		public async Task<IActionResult> PostTweet([FromBody]CreateTweetRequest request)
		{
			try
			{
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

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
