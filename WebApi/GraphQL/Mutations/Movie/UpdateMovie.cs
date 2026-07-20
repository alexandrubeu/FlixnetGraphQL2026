using Application.Dtos.Movie;
using AutoMapper;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace WebApi.GraphQL.Mutations.Movie;

[MutationType]
public class UpdateMovie
{
    public async Task<DOutputMovie> UpdateMovieAsync(
        int id,
        DInputUpdateMovie movie,
        [Service] AppDbContext context,
        [Service] IMapper mapper,
        CancellationToken ct)
    {
        var entity = await context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id, ct)
            ?? throw new KeyNotFoundException($"Movie with id {id} not found");

        mapper.Map(movie, entity);

        if (movie.GenreIds.Any())
        {
            var genres = await context.Genres
                .Where(g => movie.GenreIds.Contains(g.Id))
                .ToListAsync(ct);
            
            entity.Genres = genres;
        }

        context.Movies.Update(entity);
        await context.SaveChangesAsync(ct);

        return mapper.Map<DOutputMovie>(entity);
    }
}
