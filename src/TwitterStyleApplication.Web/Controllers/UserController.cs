using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterStyleApplication.Services.Contracts;
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
		/// Creates a user in file storage service
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
	}
}
