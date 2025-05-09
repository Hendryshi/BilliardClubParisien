using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.Interceptors;
using BCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BCP.Infrastructure.Persistence
{
    public class AppDbContext : BaseDbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            EntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
            IConfiguration configuration)
            : base(options, auditableEntitySaveChangesInterceptor)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
        public DbSet<InscriptionImage> InscriptionImages { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(m =>
            {
                m.HasKey(x => x.Id);
                m.Property(x => x.UserName).IsRequired();
                m.HasIndex(x => x.UserName).IsUnique();
                m.Property(x => x.Password).IsRequired();
            });

            modelBuilder.Entity<Inscription>(m =>
            {
                m.HasKey(x => x.Id);
                m.Property(x => x.FirstName).IsRequired();
                m.Property(x => x.LastName).IsRequired();
                m.Property(x => x.Sex).IsRequired();
                m.Property(x => x.Email).IsRequired();
                m.Property(x => x.Phone);
                m.Property(x => x.IsMemberBefore).HasDefaultValue(false);
                m.Property(x => x.Formula).IsRequired();
                m.Property(x => x.JoinCompetition).HasDefaultValue(false);
                m.Property(x => x.CompetitionCats);
                m.Property(x => x.Motivation);
                m.Property(x => x.Status).IsRequired();
            });

            modelBuilder.Entity<InscriptionImage>(m =>
            {
                m.HasKey(x => x.Id);
                m.Property(x => x.IdInscription).IsRequired();
                m.Property(x => x.ImageData).IsRequired();
                m.HasOne<Inscription>().WithMany(x => x.InscriptionImages).HasForeignKey(x => x.IdInscription).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

}
