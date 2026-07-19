namespace Domain.Entities.Movie;
using Domain.Entities.Genre;
using Domain.Entities.CastCrewCredit;
public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ImageUrl { get; set; }
    public required string VideoSource { get; set; }
    public string? TrailerUrl { get; set; }
    public bool Published { get; set; }
    public List<Genre> Genres { get; set; } = [];
    public List<CastCrewCredit> CastAndCrew { get; set; } = [];
}