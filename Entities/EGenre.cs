namespace Entities
{
    public class EGenre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<EMovie> Movies { get; set; } = new List<EMovie>();
    }
}