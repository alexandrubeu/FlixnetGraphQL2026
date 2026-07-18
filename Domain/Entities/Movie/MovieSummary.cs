namespace Domain.Entities.Movie;

public class MovieSummary
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> GenreNames { get; set; } = [];
}