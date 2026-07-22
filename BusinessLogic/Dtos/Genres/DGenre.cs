namespace BusinessLogic.Dtos
{
    public class DGenre
    {
        public DGenre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}