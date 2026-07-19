using Application.Dtos.CastCrewCredit;
using Application.Dtos.Genre;

namespace Application.Dtos.Movie;

public class DOutputMovie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ImageUrl { get; set; }
    public List<DInputCreateGenre> Genre { get; set; } = [];
    public List<DCastCrewCredit> CastAndCrew { get; set; } = [];
}