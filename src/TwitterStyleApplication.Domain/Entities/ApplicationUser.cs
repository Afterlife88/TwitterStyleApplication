using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TwitterStyleApplication.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser()
		{
			this.Following = new List<Subscription>();
			this.Followers = new List<Subscription>();
		}
		[Required]
		public override string Email { get; set; }
		public List<Subscription> Following { get; set; }

		public List<Subscription> Followers { get; set; }
	}
}
