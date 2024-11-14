using BCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BCP.Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
			
		}

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("dbo");

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(m =>
			{
				m.HasKey(x => x.Id);
				m.Property(x => x.UserName).IsRequired();
				m.Property(x => x.PasswordHash).IsRequired();
				m.Property(x => x.PasswordSalt).IsRequired();
			});
		}
	}
}
