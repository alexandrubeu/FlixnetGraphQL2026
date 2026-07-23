using BusinessLogic.Dtos;
using Entities;
using Repository;

namespace BusinessLogic
{
    public class GenresService : IGenresService
    {
        private readonly IGenreRepository _genreRepository;

        public GenresService(IGenreRepository genreRepository) =>
            _genreRepository = genreRepository;

        public IEnumerable<DGenre> GetAll() => _genreRepository.GetAll().Select(MapToDto);

        public DGenre? GetById(int id)
        {
            var genre = _genreRepository.GetById(id);
            return genre is null ? null : MapToDto(genre);
        }

        public DGenre Add(DInputCreateGenre input)
        {
            var genre = new EGenre { Name = input.Name };
            _genreRepository.Add(genre); // genre.Id gets populated in-place after SaveChanges
            return MapToDto(genre);
        }

        public DGenre? Update(int id, DInputUpdateGenre input)
        {
            var existing = _genreRepository.GetById(id);
            if (existing is null)
                return null; // repo can't tell us "not found" itself, so we check first

            existing.Name = input.Name;
            _genreRepository.Update(existing);
            return MapToDto(existing);
        }

        public bool Delete(int id)
        {
            var existing = _genreRepository.GetById(id);
            if (existing is null)
                return false;

            _genreRepository.Delete(id);
            return true;
        }

        public DGenre MapToDto(EGenre genre) => new(genre.Id, genre.Name);
    }
}
