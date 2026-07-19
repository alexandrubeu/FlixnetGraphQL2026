namespace WebApi.Queries;
using WebApi.Application.Dtos.Genre;
using Data;

[QueryType]
public static class GenreByIdQuery
{
    public static DOutputGenre GetGenreById(AppDbContext dbContext, int id)
    {
        var genre = dbContext.Genre.FirstOrDefault(g => g.Id == id);
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