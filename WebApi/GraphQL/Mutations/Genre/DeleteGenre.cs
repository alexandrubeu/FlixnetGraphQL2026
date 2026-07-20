using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace WebApi.GraphQL.Mutations.Genre;

[MutationType]
public class DeleteGenre
{
    public async Task<bool> DeleteGenreAsync(
        int id,
        [Service] AppDbContext context,
        CancellationToken ct)
    {
        var genre = await context.Genres.FirstOrDefaultAsync(g => g.Id == id, ct);
        if (genre == null)
        {
            throw new KeyNotFoundException($"Genre with ID {id} not found.");
        }

        context.Genres.Remove(genre);
        await context.SaveChangesAsync(ct);

        return true;
    }
}