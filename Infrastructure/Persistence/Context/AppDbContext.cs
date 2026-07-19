using Domain.Entities.Collection;
using Domain.Entities.Genre;
using Domain.Entities.Movie;

namespace Infrastructure.Persistence.Context;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
{
    public DbSet<Collection> ECollections { get; set; }
    public DbSet<Genre> EGenres { get; set; }
    public DbSet<Movie> EMovies { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<>()
            .HasOne()
            .WithOne()
            .HasForeignKey<>();
    }
}