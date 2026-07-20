using Application.Dtos.Collection;
using AutoMapper;
using Domain.Entities.Movie;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace WebApi.GraphQL.Mutations.Collection;

[MutationType]
public class UpdataCollection
{
    public async Task<DOutputCollection> UpdateCollectionAsync(
        int id,
        DInputUpdateCollection input,
        [Service] AppDbContext context,
        [Service] IMapper mapper,
        CancellationToken ct)
    {
        var collection = await context.Collections.FirstOrDefaultAsync(item => item.Id == id, ct);
        if (collection == null)
        {
            throw new KeyNotFoundException($"Collection with ID {id} not found.");
        }

        mapper.Map(input, collection);
        collection.Movies = await LoadMovieSummariesAsync(context, input.MovieIds, ct);

        await context.SaveChangesAsync(ct);

        return mapper.Map<DOutputCollection>(collection);
    }

    private static async Task<List<MovieSummary>> LoadMovieSummariesAsync(
        AppDbContext context,
        IReadOnlyCollection<int> movieIds,
        CancellationToken ct)
    {
        var distinctIds = movieIds.Distinct().ToArray();

        var movies = await context.Movies
            .Where(movie => distinctIds.AsEnumerable().Contains(movie.Id))
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