namespace Application.Dtos.Collection;

public class DInputUpdateCollection
{
    public required string Name { get; set; }
    public bool Published { get; set; }
    public List<int> MovieIds { get; set; } = [];
}