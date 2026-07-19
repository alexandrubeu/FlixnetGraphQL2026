using HotChocolate;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace WebApi.Mutation;

[MutationType]
public class DeleteCollection
{
    public async Task<bool> DeleteCollectionAsync(
        int id,
        [Service] AppDbContext context,
        CancellationToken ct)
    {
        var collection = await context.Collections.FirstOrDefaultAsync(item => item.Id == id, ct);
        if (collection == null)
        {
            throw new KeyNotFoundException($"Collection with ID {id} not found.");
        }

        context.Collections.Remove(collection);
        await context.SaveChangesAsync(ct);

        return true;
    }
}