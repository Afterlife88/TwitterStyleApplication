using Microsoft.AspNetCore.Mvc;
using TwitterStyleApplication.Services.ServiceModels.Enums;

namespace TwitterStyleApplication.Services
{
	public static class ServiceResponseDispatcher
	{
		/// <summary>
		/// Helper method to dispatch service response to controllers on errors
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="typeOfServiceError"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public static IActionResult ExecuteServiceResponse(Controller controller, TypeOfServiceError typeOfServiceError, string message)
		{
			ActionResult result = null;
			switch (typeOfServiceError)
			{
				case TypeOfServiceError.BadRequest:
					result = controller.BadRequest(message);
					break;
				case TypeOfServiceError.ConnectionError:
					result = controller.StatusCode(500, message);
					break;
				case TypeOfServiceError.NotFound:
					result = controller.NotFound(message);
					break;
				case TypeOfServiceError.Unathorized:
					result = controller.Unauthorized();
					break;
				case TypeOfServiceError.Forbidden:
					result = controller.StatusCode(403, message);
					break;
				case TypeOfServiceError.ServiceError:
					result = controller.StatusCode(500, message);
					break;
				default:
					result = controller.Ok();
					break;
			}
			return result;
		}
	}
}
