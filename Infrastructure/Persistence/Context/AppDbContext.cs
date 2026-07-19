
using Domain.Entities.Collection;
using Domain.Entities.Genre;
using Domain.Entities.Movie;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
{
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Movie> Movies => Set<Movie>();
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //
    //     modelBuilder.Entity<>()
    //         .HasOne()
    //         .WithOne()
    //         .HasForeignKey<>();
    // }
}