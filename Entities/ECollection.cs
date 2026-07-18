namespace Entities
{
    public class ECollection
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Published { get; set; }

        public ICollection<EMovie> Movies { get; set; } = new List<EMovie>();
    }
}