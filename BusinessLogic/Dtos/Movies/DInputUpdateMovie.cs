using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class DInputUpdateMovie
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string VideoSource { get; set; } = string.Empty;

        public List<string> TrailerUrls { get; set; } = new();

        public bool Published { get; set; }

        public List<int> GenreIds { get; set; } = new();

        public List<DInputCastCrewCredit> CastAndCrew { get; set; } = new();
    }
}