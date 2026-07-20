using Entities;

namespace Repository
{
    public interface IMovieRepository
    {
        EMovie? GetById(int id);
        IEnumerable<EMovie> GetAll();
        void Add(EMovie movie);
        void Update(EMovie movie);
        void Delete(int id);
    }
}
