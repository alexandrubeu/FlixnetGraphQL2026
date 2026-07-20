using Application.Dtos.Movie;
using AutoMapper;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApi.GraphQL.Mutations.Movie;

[MutationType]
public class AddMovie
{
    public async Task<DOutputMovie> AddMovieAsync(
        DInputCreateMovie movie,
        [Service] AppDbContext context,
        [Service] IMapper mapper,
        CancellationToken ct)
    {
        var entity = mapper.Map<Domain.Entities.Movie.Movie>(movie);
        
        if (movie.Genre.Any())
        {
            var genres = await context.Genres
                .Where(g => movie.Genre.Contains(g.Name))
                .ToListAsync(ct);
            
            entity.Genres = genres;
        }

        context.Movies.Add(entity);
        await context.SaveChangesAsync(ct);

        return mapper.Map<DOutputMovie>(entity);
    }
}
