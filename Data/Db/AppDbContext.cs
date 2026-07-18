namespace Data;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Entity;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
{
    public DbSet<ECollection> ECollections { get; set; };
    public DbSet<EGenre> EGenres { get; set; };
    public DbSet<EMovie> EMovies { get; set; };
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<>()
            .HasOne()
            .WithOne()
            .HasForeignKey<>();
    }
}


