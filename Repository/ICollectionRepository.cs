using Entities;

namespace Repository
{
    public interface ICollectionRepository
    {
        ECollection? GetById(int id);
        IEnumerable<ECollection> GetAll();
        void Add(ECollection collection);
        void Update(ECollection collection);
        void Delete(int id);
    }
}
