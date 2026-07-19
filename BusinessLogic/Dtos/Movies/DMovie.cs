using BusinessLogic.Dtos.CastCrew;
using BusinessLogic.Dtos.Genres;

namespace BusinessLogic.Dtos.Movies;

public class DMovie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string VideoSource { get; set; } = string.Empty;
    public string? TrailerUrl { get; set; }
    public bool Published { get; set; }
    public List<DGenre> Genres { get; set; } = new();
    public List<DCastCrewCredit> CastAndCrew { get; set; } = new();
}
