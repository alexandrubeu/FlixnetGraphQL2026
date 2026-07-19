namespace Entities
{
    public class ECastCrewCredit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? StageName { get; set; }

        public int MovieId { get; set; }
        public EMovie Movie { get; set; } = null!;
    }
}
