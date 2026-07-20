using Entities;

namespace Repository
{
    public interface IGenreRepository
    {
        EGenre? GetById(int id);
        IEnumerable<EGenre> GetAll();
        void Add(EGenre genre);
        void Update(EGenre genre);
        void Delete(int id);
    }
}
