namespace Application.Dtos.Collection;

public class DOutputCollection
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<int> Items { get; set; } = [];
}