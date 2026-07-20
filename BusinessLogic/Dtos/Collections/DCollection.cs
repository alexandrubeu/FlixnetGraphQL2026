namespace BusinessLogic.Dtos
{
    public class DCollection
    {
        public DCollection(int id, bool published)
        {
            Id = id;
            Published = published;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Published { get; set; }
        public List<DMovieSummary> Movies { get; set; } = new();
    }
}