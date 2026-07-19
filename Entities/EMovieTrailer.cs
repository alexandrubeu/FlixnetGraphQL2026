namespace Entities
{
    public class EMovieTrailer
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;

        public int MovieId { get; set; }
        public EMovie Movie { get; set; } = null!;
    }
}
