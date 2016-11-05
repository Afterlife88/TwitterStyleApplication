﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TwitterStyleApplication.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public override string Email { get; set; }
		public virtual ICollection<ApplicationUser> Followings { get; set; }
		public virtual ICollection<ApplicationUser> Followers { get; set; }
		public virtual ICollection<Tweet> Tweets { get; set; }
	}
}
