using Domain.Entities.Movie;

namespace Domain.Interfaces;

public interface IMoviesRepository
{
    Task<IEnumerable<MovieSummary>> GetAllAsync(CancellationToken ct = default);
    Task<Movie?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Movie> AddAsync(Movie input, CancellationToken ct = default);
    Task<Movie?> UpdateAsync(int id, Movie input, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}