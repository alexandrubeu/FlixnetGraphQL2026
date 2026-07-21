using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context) => _context = context;

        public IEnumerable<EMovie> GetAll() => _context.Movies.Include(m => m.Genres).ToList();

        public EMovie? GetById(int id) =>
            _context
                .Movies.Include(m => m.Genres)
                .Include(m => m.CastAndCrew)
                .FirstOrDefault(m => m.Id == id);

        public void Add(EMovie movie)
        {
            movie.Genres = ResolveExistingGenres(movie.Genres);
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void Update(EMovie movie)
        {
            var existing = _context
                .Movies.Include(m => m.Genres)
                .Include(m => m.CastAndCrew)
                .FirstOrDefault(m => m.Id == movie.Id);

            if (existing is null)
                return;

            _context.Entry(existing).CurrentValues.SetValues(movie);

            existing.Genres.Clear();
            foreach (var genre in ResolveExistingGenres(movie.Genres))
                existing.Genres.Add(genre);

            _context.CastCrewCredits.RemoveRange(existing.CastAndCrew);
            existing.CastAndCrew = movie.CastAndCrew;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _context.Movies.Find(id);
            if (existing is null)
                return;

            _context.Movies.Remove(existing);
            _context.SaveChanges();
        }

        private List<EGenre> ResolveExistingGenres(IEnumerable<EGenre> genreStubs) =>
            genreStubs
                .Select(stub => _context.Genres.Find(stub.Id))
                .Where(g => g != null)
                .Cast<EGenre>()
                .ToList();
    }
}
