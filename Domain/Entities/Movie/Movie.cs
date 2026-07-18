namespace Domain.Entities.Movie;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ImageUrl { get; set; }
    public required string VideoSource { get; set; }
    public string? TrailerUrl { get; set; }
    public bool Published { get; set; }
    public List<Genre.Genre> Genres { get; set; } = [];
    public List<CastCrewCredit.CastCrewCredit> CastAndCrew { get; set; } = [];
}