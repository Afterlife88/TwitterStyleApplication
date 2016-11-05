using System.Collections.Generic;
using TwitterStyleApplication.Domain.Entities;

namespace TwitterStyleApplication.Services.DTO
{
	public class RelationshipDto
	{
		public List<UserDTO> Followers { get; set; }
		public List<UserDTO> Following { get; set; }
	}
}
