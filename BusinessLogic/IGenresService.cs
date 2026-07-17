using BusinessLogic.Dtos;

namespace BusinessLogic
{
    public interface IGenresService
    {
        IEnumerable<DGenre> GetAll();
        DGenre? GetById(int id);
        DGenre Add(DInputCreateGenre input);
        DGenre? Update(int id, DInputUpdateGenre input);
        bool Delete(int id);
    }
}