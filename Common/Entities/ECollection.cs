namespace Common.Entities;

public class ECollection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Published { get; set; }
    public List<EMovieSummary> Movies { get; set; } = [];
}
