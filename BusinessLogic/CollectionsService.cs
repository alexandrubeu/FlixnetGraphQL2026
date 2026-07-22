using BusinessLogic.Dtos;
using Entities;
using Repository;

namespace BusinessLogic
{
    public class CollectionsService : ICollectionsService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMovieRepository _movieRepository;

        public CollectionsService(ICollectionRepository collectionRepository, IMovieRepository movieRepository)
        {
            _collectionRepository = collectionRepository;
            _movieRepository = movieRepository;
        }

        public DCollection? GetById(int id)
        {
            var collection = _collectionRepository.GetById(id);
            return collection is null ? null : MapToDto(collection);
        }

        public Pagination<DCollection> GetAll(PaginationParam paginationParam)
        {
            var all = _collectionRepository.GetAll().ToList();

            var items = all
                .Skip(paginationParam.Page * paginationParam.PerPage)
                .Take(paginationParam.PerPage)
                .Select(MapToDto)
                .ToList();

            return new Pagination<DCollection>
            {
                TotalCount = all.Count,
                Page = paginationParam.Page,
                PerPage = paginationParam.PerPage,
                Items = items
            };
        }

        public DCollection Add(DInputCreateCollection input)
        {
            var collection = new ECollection
            {
                Name = input.Name,
                Published = input.Published,
                Movies = ResolveExistingMovies(input.MovieIds)
            };

            _collectionRepository.Add(collection);
            return MapToDto(collection);
        }

        public DCollection? Update(int id, DInputUpdateCollection input)
        {
            var existing = _collectionRepository.GetById(id);
            if (existing is null) return null;

            existing.Name = input.Name;
            existing.Published = input.Published;

            existing.Movies.Clear();
            foreach (var movie in ResolveExistingMovies(input.MovieIds))
                existing.Movies.Add(movie);

            _collectionRepository.Update(existing);
            return MapToDto(existing);
        }

        public bool Delete(int id)
        {
            var existing = _collectionRepository.GetById(id);
            if (existing is null) return false;

            _collectionRepository.Delete(id);
            return true;
        }

        private List<EMovie> ResolveExistingMovies(IEnumerable<int> movieIds) =>
            movieIds
                .Select(id => _movieRepository.GetById(id))
                .Where(m => m != null)
                .Cast<EMovie>()
                .ToList();

        private static DCollection MapToDto(ECollection collection) => new()
        {
            Id = collection.Id,
            Name = collection.Name,
            Published = collection.Published,
            Movies = collection.Movies.Select(m => new DMovieSummary(
                m.Id,
                m.Title,
                m.ImageUrl,
                m.Genres.Select(g => g.Name).ToList()
            )).ToList()
        };
    }
}