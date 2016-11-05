using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterStyleApplication.Domain.Entities
{
	public class Tweet
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		[StringLength(250)]
		public string MessageData { get; set; }
		public string AuthorId { get; set; }
		public DateTime DateCreated { get; set; }
		public virtual ApplicationUser Author { get; set; }
		//public virtual ICollection<ApplicationUser> MentionedUsers { get; set; }

	}
}
