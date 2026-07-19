namespace Common.Entities;

public class EMovie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string VideoSource { get; set; } = string.Empty;
    public string? TrailerUrl { get; set; }
    public bool Published { get; set; }
    public List<EGenre> Genres { get; set; } = [];
    public List<ECastCrewCredit> CastAndCrew { get; set; } = [];
}
