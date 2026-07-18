using Application.Dtos.CastCrewCredit;

namespace Application.Dtos.Movie;

public class DInputCreateMovie
{
    public required string Title { get; set; }
    public required string ImageUrl { get; set; }
    public required string VideoSource { get; set; }
    public string? TrailerUrl { get; set; }
    public bool Published { get; set; }
    public List<int> GenreIds { get; set; } = [];
    public List<DCastCrewCredit> CastAndCrew { get; set; } = [];
}