using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<EUser, IdentityRole<int>, int>(options)
    {
        public DbSet<EMovie> Movies { get; set; }
        public DbSet<EGenre> Genres { get; set; }
        public DbSet<ECollection> Collections { get; set; }
        public DbSet<ECastCrewCredit> CastCrewCredits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(
                optionsBuilder.UseMySQL(
                    "server=localhost;port=3306;database=flixnet;user=root;password=7410;"
                )
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // resolves the bug with user primary key

            modelBuilder.Ignore<IdentityUserPasskey<int>>(); // resolves the bug when creating a migration
            // passkeys not compatible withMYSQL probably
            // ^ is (probably) related to builder.services.AddIdentityCore
            // instead of AddIdentity
            //  it's the more lightweight option, with no extra auth options
            modelBuilder.Entity<EMovie>().ToTable("Movies");
            modelBuilder.Entity<EGenre>().ToTable("Genres");
            modelBuilder.Entity<ECollection>().ToTable("Collections");
            modelBuilder.Entity<ECastCrewCredit>().ToTable("CastCrewCredits");

            modelBuilder.Entity<EMovie>().Property(e => e.Id).IsRequired();
            modelBuilder.Entity<EGenre>().Property(e => e.Id).IsRequired();
            modelBuilder.Entity<ECollection>().Property(e => e.Id).IsRequired();
            modelBuilder.Entity<ECastCrewCredit>().Property(e => e.Id).IsRequired();

            modelBuilder.Entity<EMovie>().HasMany(m => m.Genres).WithMany(g => g.Movies);

            modelBuilder.Entity<EMovie>().HasMany(m => m.Collections).WithMany(c => c.Movies);

            modelBuilder
                .Entity<ECastCrewCredit>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.CastAndCrew)
                .HasForeignKey(c => c.MovieId);
        }
    }
}
