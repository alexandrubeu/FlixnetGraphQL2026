namespace BusinessLogic.Dtos
{
    public class DMovie
    {
        // public DMovie(int id, string? trailerUrl, bool published, DateTime createdAt, List<DGenre>?  genres)
        // {
        //     Id = id;
        //     TrailerUrl = trailerUrl;
        //     Published = published;
        //     Genres = genres;
        //     CreatedAt = createdAt;
        // }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string VideoSource { get; set; } = string.Empty;
        public string? TrailerUrl { get; set; }
        public bool Published { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public List<DGenre> Genres { get; set; } = new();
        public List<DCastCrewCredit> CastAndCrew { get; set; } = new();
    }
}
