using BusinessLogic.Dtos.Genres;

namespace BusinessLogic.Dtos.Movies;

public class DMovieSummary
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<DGenre> Genres { get; set; } = [];
}
