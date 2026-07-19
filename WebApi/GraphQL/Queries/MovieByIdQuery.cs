using Application.Dtos.CastCrewCredit;
using Application.Dtos.Genre;
using Application.Dtos.Movie;
using Infrastructure.Persistence.Context;

namespace WebApi.Queries;

[QueryType]
public static class MovieByIdQuery
{
    public static DOutputMovie GetMovieById(AppDbContext dbContext, int id)
    {
        var movie = dbContext.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
        {
            throw new Exception($"Movie with ID {id} not found.");
        }

        return new DOutputMovie
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
        };
    }
}