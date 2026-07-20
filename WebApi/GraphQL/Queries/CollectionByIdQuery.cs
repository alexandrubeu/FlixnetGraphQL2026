namespace WebApi.GraphQL.Queries;

public static class CollectionByIdQuery
{
    public static DOutputCollection GetCollection(AppDbContext dbContext, int id)
    {
        var collection = dbContext.Collections.FirstOrDefault(c => c.Id == id);
        if (collection == null)
        {
            throw new Exception($"Collection with ID {id} not found.");
        }

        return new DOutputCollection
        {
            Id = collection.Id,
            Name = collection.Name,
            //Items = collection.MovieIds
        };
    }
}