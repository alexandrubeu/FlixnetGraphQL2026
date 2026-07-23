using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly AppDbContext _context;

        public CollectionRepository(AppDbContext context) => _context = context;

        public IEnumerable<ECollection> GetAll() =>
            _context.Collections.Include(c => c.Movies).ThenInclude(m => m.Genres).ToList();

        public ECollection? GetById(int id) =>
            _context
                .Collections.Include(c => c.Movies)
                    .ThenInclude(m => m.Genres)
                .FirstOrDefault(c => c.Id == id);

        public void Add(ECollection collection)
        {
            collection.Movies = ResolveExistingMovies(collection.Movies);
            _context.Collections.Add(collection);
            _context.SaveChanges();
        }

        public void Update(ECollection collection)
        {
            var existing = _context
                .Collections.Include(c => c.Movies)
                .FirstOrDefault(c => c.Id == collection.Id);

            if (existing is null)
                return;

            existing.Name = collection.Name;
            existing.Published = collection.Published;

            existing.Movies.Clear();
            foreach (var movie in ResolveExistingMovies(collection.Movies))
                existing.Movies.Add(movie);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _context.Collections.Find(id);
            if (existing is null)
                return;

            _context.Collections.Remove(existing);
            _context.SaveChanges();
        }

        private List<EMovie> ResolveExistingMovies(IEnumerable<EMovie> movieStubs) =>
            movieStubs
                .Select(stub => _context.Movies.Find(stub.Id))
                .Where(m => m != null)
                .Cast<EMovie>()
                .ToList();
    }
}
