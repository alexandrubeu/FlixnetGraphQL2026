namespace WebApi.GraphQL.Queries;

[QueryType]
public static class GenresQuery
{
    public static IEnumerable<DOutputGenre> GetGenres(AppDbContext dbContext)
    {
        return dbContext.Genres.Select(genre => new DOutputGenre
        {
            Id = genre.Id,
            Name = genre.Name
        }).ToList();
    }
}