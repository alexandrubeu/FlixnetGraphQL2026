namespace BusinessLogic.Dtos
{
    public class DInputUpdateMovie
    {
        public required string Title { get; set; } = string.Empty;

        public required string ImageUrl { get; set; } = string.Empty;

        public required string VideoSource { get; set; } = string.Empty;

        public string? TrailerUrl { get; set; }

        public bool Published { get; set; }

        public List<int> GenreIds { get; set; } = [];

        public List<DInputCastCrewCredit> CastAndCrew { get; set; } = [];
    }
}