using System.ComponentModel.DataAnnotations;
using BusinessLogic.Dtos.CastCrew;

namespace BusinessLogic.Dtos.Movies;

public class DInputUpdateMovie
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public string VideoSource { get; set; } = string.Empty;

    public string? TrailerUrl { get; set; }

    public bool Published { get; set; }

    public List<int> GenreIds { get; set; } = new();

    public List<DInputCastCrewCredit> CastAndCrew { get; set; } = new();
}
