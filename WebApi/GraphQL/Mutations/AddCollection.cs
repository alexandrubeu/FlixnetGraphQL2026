using Application.Dtos.Collection;
using Infrastructure.Persistence.Context;

namespace WebApi.Mutation;

[MutationType]
public class AddCollection
{
    public async Task<DCollection> AddCollectionAsync(DInputCreateCollection collection, AppDbContext context, CancellationToken ct)
    {
        context.Collections.Add(collection);
        await context.SaveChangesAsync();
        return collection;
    }
}
