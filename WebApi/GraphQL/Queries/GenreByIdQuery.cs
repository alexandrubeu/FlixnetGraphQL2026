namespace WebApi.GraphQL.Queries;

[QueryType]
public static class GenreByIdQuery
{
    public static DOutputGenre GetGenreById(AppDbContext dbContext, int id)
    {
        var genre = dbContext.Genres.FirstOrDefault(g => g.Id == id);
        if (genre == null)
        {
            throw new Exception($"Genre with ID {id} not found.");
        }

        return new DOutputGenre
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
}