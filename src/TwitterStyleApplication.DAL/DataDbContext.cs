using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TwitterStyleApplication.Domain.Entities;

namespace TwitterStyleApplication.DAL
{
	public class DataDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Tweet> Tweets { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; } 
		public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
		{ }


		protected override void OnModelCreating(ModelBuilder builder)
		{

			base.OnModelCreating(builder);

			//builder.Entity<Subscription>()
			//	.HasKey(s => new { s.PublisherId, s.SubscriberId });

			builder.Entity<Subscription>()
			   .HasOne(s => s.Publisher)
			   .WithMany(au => au.Following)
			   .OnDelete(DeleteBehavior.Restrict)
			   .HasForeignKey(s => s.PublisherId);

			builder.Entity<Subscription>()
				.HasOne(s => s.Subscriber)
				.WithMany(au => au.Followers)
				.OnDelete(DeleteBehavior.Restrict)
				.HasForeignKey(s => s.SubscriberId);
			//builder.Entity<ApplicationUser>().HasMany(s => s.Followers);
			//builder.Entity<ApplicationUser>().HasMany(s => s.Following);
			//base.OnModelCreating(builder);
		}

	}
}
