namespace WebApi.Queries;
using WebApi.Application.Dtos.Genre;
using Data;

[QueryType]
public static class GenresQuery
{
    public static IEnumerable<DOutputGenre> GetGenres(AppDbContext dbContext)
    {
        return dbContext.Genre.Select(genre => new DOutputGenre
        {
            Id = genre.Id,
            Name = genre.Name
        }).ToList();
    }
}