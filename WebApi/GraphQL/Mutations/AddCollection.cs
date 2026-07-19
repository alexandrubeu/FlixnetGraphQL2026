using Application.Dtos.Collection;
using AutoMapper;
using Domain.Entities.Movie;
using HotChocolate;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace WebApi.Mutation;

[MutationType]
public class AddCollection
{
    public async Task<DOutputCollection> AddCollectionAsync(
        DInputCreateCollection collection,
        [Service] AppDbContext context,
        [Service] IMapper mapper,
        CancellationToken ct)
    {
        var entity = mapper.Map<Domain.Entities.Collection.Collection>(collection);
        entity.Movies = await LoadMovieSummariesAsync(context, collection.MovieIds, ct);

        context.Collections.Add(entity);
        await context.SaveChangesAsync(ct);

        return mapper.Map<DOutputCollection>(entity);
    }

    private static async Task<List<MovieSummary>> LoadMovieSummariesAsync(
        AppDbContext context,
        IReadOnlyCollection<int> movieIds,
        CancellationToken ct)
    {
        var distinctIds = movieIds.Distinct().ToArray();

        var movies = await context.Movies
            .Where(movie => distinctIds.Contains(movie.Id))
            .Include(movie => movie.Genres)
            .ToListAsync(ct);

        if (movies.Count == distinctIds.Length)
            return movies.Select(movie => new MovieSummary
            {
                Id = movie.Id,
                Title = movie.Title,
                ImageUrl = movie.ImageUrl,
                GenreNames = movie.Genres.Select(genre => genre.Name).ToList()
            }).ToList();
        {
            var foundIds = movies.Select(movie => movie.Id).ToHashSet();
            var missingIds = distinctIds.Where(id => !foundIds.Contains(id)).ToArray();

            throw new KeyNotFoundException($"Movies not found: {string.Join(", ", missingIds)}");
        }

    }
}
