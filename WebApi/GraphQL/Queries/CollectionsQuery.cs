using Application.Dtos.Collection;
using Infrastructure.Persistence.Context;

namespace WebApi.GraphQL.Queries;
[QueryType]
public static class CollectionsQuery
{
    public static IEnumerable<DOutputCollection> GetCollections(AppDbContext dbContext)
    {
        return dbContext.Collections.Select(collection => new DOutputCollection
        {
            Id = collection.Id,
            Name = collection.Name,
            //Items = collection.MovieIds
        }).ToList();
    }
}