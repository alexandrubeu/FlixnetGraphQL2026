namespace Entities
{
    public class EMovie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string VideoSource { get; set; } = string.Empty;
        public string? TrailerUrl { get; set; }
        public bool Published { get; set; }

        public ICollection<EGenre> Genres { get; set; } = new List<EGenre>();
        public ICollection<ECastCrewCredit> CastAndCrew { get; set; } = new List<ECastCrewCredit>();
        public ICollection<ECollection> Collections { get; set; } = new List<ECollection>();
    }
}