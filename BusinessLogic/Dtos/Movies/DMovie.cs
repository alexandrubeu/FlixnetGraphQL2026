namespace BusinessLogic.Dtos
{
    public class DMovie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string VideoSource { get; set; } = string.Empty;
        public List<string> TrailerUrls { get; set; } = new();
        public bool Published { get; set; }
        public List<DGenre> Genres { get; set; } = new();
        public List<DCastCrewCredit> CastAndCrew { get; set; } = new();
    }
}