using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace WebApi.GraphQL.Mutations.Movie;

[MutationType]
public class DeleteMovie
{
    public async Task<bool> DeleteMovieAsync(
        int id,
        [Service] AppDbContext context,
        CancellationToken ct)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id, ct);
        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with ID {id} not found.");
        }

        context.Movies.Remove(movie);
        await context.SaveChangesAsync(ct);

        return true;
    }
}
