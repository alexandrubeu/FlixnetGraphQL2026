namespace Common.Entities;

public class EMovieSummary
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<EGenre> Genres { get; set; } = [];
}
