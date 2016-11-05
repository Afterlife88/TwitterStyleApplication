using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterStyleApplication.Domain.Entities;

namespace TwitterStyleApplication.DAL
{
    public class DataDbContext : IdentityDbContext<ApplicationUser>
    {
		public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
		{ }
	}
}
