using BusinessLogic;
using BusinessLogic.Dtos;
using Repository;
using WebApi.GraphQL.Cursor;
using Microsoft.EntityFrameworkCore;

namespace WebApi.GraphQL.Services;

public class MovieServiceWithCursor(AppDbContext context,GenresService genresService) : IMovieServiceWithCursor
{
    private readonly AppDbContext _context = context;
    private readonly GenresService _genresService = genresService;
    public Task<PagedResult<DMovie>> GetMoviesAsync(int pageSize, string? afterCursor)
    {
        var query = _context.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(afterCursor))
        {
            var parsedCursor = CursorEncoder.Decoder(afterCursor);
            if (parsedCursor.HasValue)
            {
                var (cursorDate, cursorId) = parsedCursor.Value;
                query = query.Where(movie => movie.CreatedAt < cursorDate
                                             || (movie.CreatedAt == cursorDate && movie.Id == cursorId)
                );
            }
        }

        query = query.OrderByDescending(movie => movie.CreatedAt).ThenByDescending(movie => movie.Id);
        
        var movies = query.Take(pageSize + 1).ToList();
        var hasNextPage = movies.Count > pageSize;
        var actualMovies = movies.Take(pageSize).ToList();
        
        var dMovies = actualMovies.Select(movie =>
        {
            var genres = movie.Genres.Select(g => _genresService.MapToDto(g)).ToList();
            return new DMovie()
            {
                Id = movie.Id,
                TrailerUrl = movie.TrailerUrl,
                Published = movie.Published,
                CreatedAt = movie.CreatedAt,
                Genres = genres
            };
        }).ToList();

        var endCursor = actualMovies.Any()
            ? CursorEncoder.Encoder(actualMovies.Last().CreatedAt, actualMovies.Last().Id)
            : null;

        var result = new PagedResult<DMovie>
        {
            Items = dMovies,
            HasNextPage = hasNextPage,
            HasPreviousPage = !string.IsNullOrEmpty(afterCursor),
            EndCursor = endCursor
        };

        return Task.FromResult(result);
    }
    
}