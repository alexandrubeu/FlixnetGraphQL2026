using BusinessLogic.Dtos.Movies;

namespace BusinessLogic.Dtos.Collections;

public class DCollection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Published { get; set; }
    public List<DMovieSummary> Movies { get; set; } = new();
}