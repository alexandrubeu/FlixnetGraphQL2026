namespace WebApi.Queries;
using WebApi.Application.Dtos.Collection;
using WebApi.Domain.Entities.Collection;
using Data;

[QueryType]
public static class CollectionsQuery
{
    public static IEnumerable<DOutputCollection> GetCollections(AppDbContext dbContext)
    {
        return dbContext.Collection.Select(collection => new DOutputCollection
        {
            Id = collection.Id,
            Name = collection.Name,
            Items = collection.MovieIds
        }).ToList();
    }
}

//to add later in CollectionByIdQuery 
/*public static DOutputCollection GetCollection(AppDbContext dbContext, int id)
    {
        var collection = dbContext.Collection.FirstOrDefault(c => c.Id == id);
        if (collection == null)
        {
            throw new Exception($"Collection with ID {id} not found.");
        }

        return new DOutputCollection
        {
            Id = collection.Id,
            Name = collection.Name,
            Items = collection.MovieIds
        };
    }*/