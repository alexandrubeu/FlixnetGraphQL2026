namespace BusinessLogic.Dtos
{
    public class DMovieSummary
    {
        public DMovieSummary(int id, string title, string img, List<string> genres)
        {
            Id = id;
            Title = title;
            ImageUrl = img;
            GenreNames = genres;
        }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> GenreNames { get; set; } = new();
    }
}
