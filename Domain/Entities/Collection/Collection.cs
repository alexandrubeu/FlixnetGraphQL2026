using Domain.Entities.Movie;

namespace Domain.Entities.Collection;

public class Collection
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool Published { get; set; }
    public List<MovieSummary> Movies { get; set; } = [];
}