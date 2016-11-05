using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterStyleApplication.Domain.Entities;

namespace TwitterStyleApplication.DAL
{
	public class DataDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Tweet> Tweets { get; set; }
		public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ApplicationUser>().HasMany(x => x.Followers);
			builder.Entity<ApplicationUser>().HasMany(x => x.Followings);
			builder.Entity<ApplicationUser>().HasMany(x => x.Tweets);
			base.OnModelCreating(builder);
		}

	}
}
