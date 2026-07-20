namespace WebApi.GraphQL.Queries;

[QueryType]
public static class MoviesQuery
{
    public static IEnumerable<DOutputMovie> GetMovies(AppDbContext dbContext, string terms)
    {
        return dbContext.Movies.Select(movie => new DOutputMovie
        {
            Id = movie.Id,
            Title = movie.Title,
            ImageUrl = movie.ImageUrl,
            Genre = movie.Genres.Select(genre => new DInputCreateGenre
            {
                Name = genre.Name
            }).ToList(),
            CastAndCrew = movie.CastAndCrew.Select(castCrew => new DCastCrewCredit
            {
                Name = castCrew.Name,
                Role = castCrew.Role,
                StageName = castCrew.StageName
            }).ToList()
        }).ToList();
    }
}