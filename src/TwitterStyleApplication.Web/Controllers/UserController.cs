using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterStyleApplication.DAL.Contracts;
using TwitterStyleApplication.Services;
using TwitterStyleApplication.Services.Contracts;
using TwitterStyleApplication.Services.DTO;
using TwitterStyleApplication.Services.RequestModels;

namespace TwitterStyleApplication.Web.Controllers
{
	[Route("api/users")]
	public class UsersController : Controller
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		/// <summary>
		/// Creates a user
		/// </summary>
		/// <remarks>
		/// 
		/// <b>Creating the user in service</b>
		/// 
		/// </remarks>
		/// <param name="model"></param>
		/// <response code="201">Returns if user created successfully</response>
		/// <response code="400">Returns if some required fields are missing in request</response>
		/// <response code="500">Returns if server error has occurred</response>
		[HttpPost]
		[ProducesResponseType(typeof(StatusCodeResult), 201)]
		[ProducesResponseType(typeof(BadRequestResult), 400)]
		[ProducesResponseType(typeof(InternalServerErrorResult), 500)]
		public async Task<IActionResult> Register([FromBody] RegistrationRequest model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var serviceResponse = await _userService.CreateAsync(model);

				if (serviceResponse.IsValid)
				{
					return StatusCode(201);
				}
				return BadRequest(serviceResponse.ErrorMessage);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// Return all user with email and username
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(ActiveAuthenticationSchemes = "Bearer")]
		public async Task<IActionResult> AllUsers()
		{
			try
			{
				var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

				var serviceResponse = await _userService.AllUser(currentUser);


				if (!_userService.State.IsValid)
					return ServiceResponseDispatcher.ExecuteServiceResponse(this, _userService.State.TypeOfError,
						_userService.State.ErrorMessage);
				return Ok(serviceResponse);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// Follow requested user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize(ActiveAuthenticationSchemes = "Bearer")]
		[Route("follow")]
		[HttpPost]
		public async Task<IActionResult> Follow([FromBody]FollowUnfollowRequest request)
		{
			try
			{
				var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

				var response = await _userService.FollowUser(currentUser, request.Username);

				if (!_userService.State.IsValid)
					return ServiceResponseDispatcher.ExecuteServiceResponse(this, _userService.State.TypeOfError,
						_userService.State.ErrorMessage);

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// Get follow and unfollow user list
		/// </summary>
		/// <returns></returns>
		[Authorize(ActiveAuthenticationSchemes = "Bearer")]
		[Route("relationship")]
		[HttpGet]
		[ProducesResponseType(typeof(RelationshipDto), 200)]
		public async Task<IActionResult> GetRelationshipToUser()
		{
			try
			{
				var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;

				var serviceResponse = await _userService.GetRelationship(userEmail);
				if (!_userService.State.IsValid)
					return ServiceResponseDispatcher.ExecuteServiceResponse(this, _userService.State.TypeOfError,
						_userService.State.ErrorMessage);

				return Ok(serviceResponse);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// Unfollow requested user
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[Route("unfollow")]
		[HttpPost]
		public async Task<IActionResult> Unfollow([FromBody] FollowUnfollowRequest request)
		{
			try
			{
				var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

				var response = await _userService.UnFollowUser(currentUser, request.Username);


				if (!_userService.State.IsValid)
					return ServiceResponseDispatcher.ExecuteServiceResponse(this, _userService.State.TypeOfError,
						_userService.State.ErrorMessage);

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
