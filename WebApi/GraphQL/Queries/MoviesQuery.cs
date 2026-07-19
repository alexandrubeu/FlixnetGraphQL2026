namespace WebApi.Queries;
using WebApi.Application.Dtos.Movie;
using WebApi.Application.Dtos.Genre;
using WebApi.Application.Dtos.CastCrew;
using WebApi.Domain.Entities.Movie;
using Data;

[QueryType]
public static class MoviesQuery
{
    public static IEnumerable<DOutputMovie> GetMovies(AppDbContext dbContext, string terms)
    {
        return dbContext.Movie.Select(movie => new DOutputMovie
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