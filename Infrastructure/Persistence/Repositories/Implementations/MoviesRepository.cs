using Domain.Entities.Movie;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Implementations;

public class MoviesRepository(AppDbContext _db) : IMoviesRepository
{
    public async Task<IEnumerable<MovieSummary>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Movies
            .Select(m => new MovieSummary
            {
                Id = m.Id
            })
            .ToListAsync(ct);
    }

    public async Task<Movie?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _db.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task<Movie> AddAsync(Movie input, CancellationToken ct = default)
    {
        try
        {
            _db.Movies.Add(input);
            await _db.SaveChangesAsync(ct);
            return input;
        }
        catch (DbUpdateException)
        {
            throw new AbandonedMutexException("Create movie failed");
        }
    }

    public async Task<Movie?> UpdateAsync(int id, Movie input, CancellationToken ct = default)
    {
        try
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id, ct);

            if (movie is null)
                return null;

            movie = input;

            await _db.SaveChangesAsync(ct);

            return movie;
        }
        catch (DbUpdateException)
        {
            throw new AbandonedMutexException("Update movie failed");
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id, ct);

            if (movie is null)
                return false;

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync(ct);

            return true;
        }
        catch (DbUpdateException)
        {
            throw new AbandonedMutexException("Delete movie failed");
        }
    }
}