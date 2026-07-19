namespace WebApi.Mutation;
using BusinessLogic.Dtos; 
using Data;


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
