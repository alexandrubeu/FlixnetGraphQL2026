using Common.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos;

public class MoviesRepository(IDbContextFactory<DataBaseContext> dbContextFactory)
    : IMoviesRepository
{
    public IEnumerable<EMovie> GetAll()
    {
        using var context = dbContextFactory.CreateDbContext();
        return context.Movies.ToList();
    }
}
