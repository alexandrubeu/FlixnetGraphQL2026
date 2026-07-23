using Entities;

namespace Repository
{
    public interface IGenreRepository
    {
        IEnumerable<EGenre> GetAll();
        //IQueryable<EGenre> GetAll();
        EGenre? GetById(int id);
        void Add(EGenre genre);
        void Update(EGenre genre);
        void Delete(int id);
    }
}
