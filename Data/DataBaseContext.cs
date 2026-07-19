using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
{
    public DbSet<EMovie> Movies { get; set; }
    public DbSet<EGenre> Genres { get; set; }
    public DbSet<ECollection> Collections { get; set; }
    public DbSet<ECastCrewCredit> Credits { get; set; }
}
