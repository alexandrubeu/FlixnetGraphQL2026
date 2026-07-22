using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EMovie> Movies { get; set; }
        public DbSet<EGenre> Genres { get; set; }
        public DbSet<ECollection> Collections { get; set; }
        public DbSet<ECastCrewCredit> CastCrewCredits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseMySQL("server=localhost;port=3306;database=flixnet;user=root;password=7410;"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EMovie>().ToTable("Movies");
            modelBuilder.Entity<EGenre>().ToTable("Genres");
            modelBuilder.Entity<ECollection>().ToTable("Collections");
            modelBuilder.Entity<ECastCrewCredit>().ToTable("CastCrewCredits");

            modelBuilder.Entity<EMovie>().Property(e => e.Id).IsRequired();
            modelBuilder.Entity<EGenre>().Property(e => e.Id).IsRequired();
            modelBuilder.Entity<ECollection>().Property(e => e.Id).IsRequired();
            modelBuilder.Entity<ECastCrewCredit>().Property(e => e.Id).IsRequired();

            modelBuilder.Entity<EMovie>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies);

            modelBuilder.Entity<EMovie>()
                .HasMany(m => m.Collections)
                .WithMany(c => c.Movies);

            modelBuilder.Entity<ECastCrewCredit>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.CastAndCrew)
                .HasForeignKey(c => c.MovieId);
        }
    }
}