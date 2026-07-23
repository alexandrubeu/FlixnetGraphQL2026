using Entities;

namespace Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context) => _context = context;

        public IEnumerable<EGenre> GetAll() => _context.Genres.ToList();
        //public IQueryable<EGenre> GetAll() => _context.Genres;

        public EGenre? GetById(int id) => _context.Genres.Find(id);

        public void Add(EGenre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void Update(EGenre genre)
        {
            var existing = _context.Genres.Find(genre.Id);
            if (existing is null)
                return;

            existing.Name = genre.Name;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _context.Genres.Find(id);
            if (existing is null)
                return;

            _context.Genres.Remove(existing);
            _context.SaveChanges();
        }
    }
}
