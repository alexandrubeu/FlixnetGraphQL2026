namespace Domain.Entities.CastCrewCredit;

public class CastCrewCredit
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    public string? StageName { get; set; }
}