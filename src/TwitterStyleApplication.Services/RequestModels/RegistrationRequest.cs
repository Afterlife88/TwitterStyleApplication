﻿using System.ComponentModel.DataAnnotations;

namespace TwitterStyleApplication.Services.RequestModels
{
	public class RegistrationRequest
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Required]
		[Display(Name = "User Name")]
		public string UserName { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
	}
}
